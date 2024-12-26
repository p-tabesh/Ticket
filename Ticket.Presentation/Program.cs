using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Prometheus;
using Ticket.Application.Services;
using Ticket.Domain.IRepository;
using Ticket.Infrastructure;
using Ticket.Infrastructure.Context;
using Ticket.Infrastructure.Repository;
using Ticket.Infrastructure.UnitOfWork;
using Ticket.Infrastructure.UnitsOfWork;


var builder = WebApplication.CreateBuilder(args);




// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TicketDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString"));
    });

builder.Services.AddStackExchangeRedisCache(
        option =>
        {
            builder.Configuration.GetConnectionString("RedisConnectionString");
        });

builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<TicketService>();
builder.Services.AddScoped<TicketUnitOfWork>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryFieldRepository, CategoryFieldRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped(typeof(IGenericRepositoy<>), typeof(GenericRepository<>));



var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMetricServer();
    app.UseHttpMetrics();
}

//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");
//    app.UseHsts();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
