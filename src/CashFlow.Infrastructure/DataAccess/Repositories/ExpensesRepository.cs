using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Infrastructure.DataAccess.Repositories
{
    // Infraestrutura enxerga Domain, então temos acesso à Interface. O projeto de API também enxerga Infraestrutura, então definimos
    // como Internal para garantir que esse repositório seja visível apenas aqui.
    internal class ExpensesRepository : IExpensesRepository
    {
        public void Add(Expense expense)
        {
            var dbContext = new CashflowDbContext();

            dbContext.Expenses.Add(expense);

            dbContext.SaveChanges();
        }
    }
}
