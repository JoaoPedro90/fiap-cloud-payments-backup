using Stripe;
using PaymentsService.Utils;
using PaymentsService.Services;
using PaymentsService.Repositories;
using PaymentsService.Domain.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<PaymentsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<EventStoreContextRepository>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();
builder.Services.AddScoped<IPagamentoService, PagamentoService>();
builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();

builder.Services.AddScoped<IEventStoreService, EventStoreService>();

builder.Services.AddSingleton<IEventStore, EventStore>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];



var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsProduction())
//{ 
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
