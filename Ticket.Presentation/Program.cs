using Microsoft.EntityFrameworkCore;
using Prometheus;
using RedLockNet;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;
using System.Text.Json.Serialization;
using Ticket.Domain.IUnitOfWork;
using Ticket.Infrastructure.Context;
using Ticket.Infrastructure.UnitOfWork;
using Ticket.Presentation.Middlewares;
using Ticket.Presentation.ServiceConfigurations;



var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddCors(
    option =>
    {
        option.AddPolicy("AllowAll", builder => builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
    });

builder.Services.AddSwaggerConfigurations();


// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();


// SqlServer Configuration
if (builder.Environment.EnvironmentName == "DockerEnv")
{
    builder.Services.AddDbContext<TicketDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DockerSqlConnectionString"));
    });
}
else
{
    builder.Services.AddDbContext<TicketDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString"));
    });
}


// Redis cache connection
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnectionString")));

builder.Services.AddStackExchangeRedisCache(
        option =>
        {
            option.Configuration = builder.Configuration.GetConnectionString("RedisConnectionString");
            option.InstanceName = "TicketAppCache";
        });

// Redis lock connection
var connectionMultiplexer = ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnectionString"));
var multiplexers = new List<RedLockMultiplexer>
{
    connectionMultiplexer
};

var redlockFactory = RedLockFactory.Create(multiplexers);

builder.Services.AddSingleton<IDistributedLockFactory>(redlockFactory);



// Services
builder.Services.AddServices();

// UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Repositories
builder.Services.AddRepositories();


// Exception Middleware Handler
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();


builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5000); // Ensure this matches your Docker port mapping
});


builder.Services.AddAuthentications(builder.Configuration);
builder.Services.AddAuthorizations();




var app = builder.Build();
if (app.Environment.EnvironmentName == "DockerEnv")
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<TicketDbContext>();
        dbContext.Database.Migrate();
    }
}


app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseMiddleware<JwtMiddleware>();
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers()
    .RequireAuthorization();
app.Urls.Add("http://0.0.0.0:5000");

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "DockerEnv")
//{
app.UseSwagger();
app.UseSwaggerUI();
app.UseMetricServer();
app.UseHttpMetrics();
//}



app.Run();
