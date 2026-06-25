using MeatFlow.Data.Context;
using MeatFlow.Data.Repositories.Interfaces;
using MeatFlow.Data.Repositories.Repository;
using MeatFlow.Service.Interfaces;
using MeatFlow.Service.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "MeatFlow API", Version = "v1", Description = "API para gerenciamento de pedidos de carne." });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddDbContext<MeatFlowDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEstadoRepository, EstadoRepository>();
builder.Services.AddScoped<ICidadeRepository, CidadeRepository>();
builder.Services.AddScoped<ICompradorRepository, CompradorRepository>();
builder.Services.AddScoped<ICarneRepository, CarneRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IItemPedidoRepository, ItemPedidoRepository>();

builder.Services.AddScoped<IEstadoService, EstadoService>();
builder.Services.AddScoped<ICidadeService, CidadeService>();
builder.Services.AddScoped<ICompradorService, CompradorService>();
builder.Services.AddScoped<ICarneService, CarneService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "MeatFlow API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
