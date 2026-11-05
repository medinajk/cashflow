using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess
{
    internal class CashflowDbContext : DbContext
    {
        // O construtor vai repassar para o construtor da classe base (passa para o DbContext as options recebidas)
        public CashflowDbContext(DbContextOptions options) : base(options) { }

        // Acesso à tabela Expenses no banco de dados
        public DbSet<Expense> Expenses { get; set; }
    }
}
