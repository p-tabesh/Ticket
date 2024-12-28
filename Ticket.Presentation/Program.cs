using Microsoft.EntityFrameworkCore;
using Prometheus;
using Ticket.Application.Mappings;
using Ticket.Application.Services;
using Ticket.Domain.IRepository;
using Ticket.Domain.IUnitOfWork;
using Ticket.Infrastructure.Context;
using Ticket.Infrastructure.Repository;
using Ticket.Infrastructure.UnitOfWork;



var builder = WebApplication.CreateBuilder(args);


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

// Mapper Configuration
builder.Services.AddAutoMapper(typeof(TicketMappingProfile));


// Redis Connection
//builder.Services.AddStackExchangeRedisCache(
//        option =>
//        {
//            builder.Configuration.GetConnectionString("RedisConnectionString");
//        });


// Services
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<TicketService>();
builder.Services.AddScoped<UserService>();
// UnitOfWork
builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork<TicketDbContext>));
builder.Services.AddScoped(typeof(IGenericRepositoy<>), typeof(GenericRepository<>));
// Repositories

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryFieldRepository, CategoryFieldRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();



var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMetricServer();
    app.UseHttpMetrics();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
