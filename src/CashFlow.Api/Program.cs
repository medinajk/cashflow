// classe mãe da api, onde é configurada a aplicação, os serviços e o pipeline de requisições

using CashFlow.Api.Filters;
using CashFlow.Api.Middleware;
using CashFlow.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// typeof -> o objeto é criado DEPOIS, só quando uma exceção ocorrer
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter))); // adiciona o ExceptionFilter como um filtro global. Sem essa linha, o ExceptionFilter existe mas nunca seria chamado.


builder.Services.AddInfraestructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>(); // adiciona o CultureMiddleware ao pipeline de requisições. Sem essa linha, o CultureMiddleware existe mas nunca seria chamado.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
