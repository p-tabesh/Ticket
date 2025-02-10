using Microsoft.EntityFrameworkCore;
using Prometheus;
using Ticket.Application.Services;
using Ticket.Domain.IRepository;
using Ticket.Domain.IUnitOfWork;
using Ticket.Infrastructure.Context;
using Ticket.Infrastructure.Repository;
using Ticket.Infrastructure.UnitOfWork;
using Ticket.Presentation.Middlewares;
using RedLockNet;
using StackExchange.Redis;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Ticket.Presentation.ServiceConfigurations;



var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddCors(
    option =>
    {
        option.AddPolicy("AllowAll", builder => builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
    });

// Add services to the container.
builder.Services.AddControllers();


// Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SqlServer Configuration
builder.Services.AddDbContext<TicketDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString"));
    });

//builder.Services.AddDbContextFactory<TicketDbContext>(
//    options =>
//    {
//        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString"));
//    });



// Redis Connection
//builder.Services.AddStackExchangeRedisCache(
//        option =>
//        {
//            builder.Configuration.GetConnectionString("RedisConnectionString");
//        });

var multiplexer = new List<RedLockMultiplexer>
{
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnectionString"))
};

var redlockFactory = RedLockFactory.Create(multiplexer);
builder.Services.AddSingleton<IDistributedLockFactory>(redlockFactory);

// Services
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<TicketService>();
builder.Services.AddScoped<UserService>();

// UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Repositories
builder.Services.AddRepositories();


// Exception Middleware Handler
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

// Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option => option.Cookie.SameSite = SameSiteMode.Strict);

// Need Authorize for all controllers
//builder.Services.AddControllers(c => c.Filters.Add(new AuthorizeFilter())); 

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5000); // Ensure this matches your Docker port mapping
});

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<TicketDbContext>();
//    dbContext.Database.Migrate();
//}

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Urls.Add("http://0.0.0.0:5000");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMetricServer();
    app.UseHttpMetrics();
}

app.Run();
