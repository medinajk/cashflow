using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfraestructure(this IServiceCollection services)
        {
            // Registrando a Injeção de dependência
            // Faz a instância da classe após a vírgula (Interfaces não podem ser instanciadas) - modularização
            services.AddScoped<IExpensesRepository, ExpensesRepository>();
        }
    }
}
