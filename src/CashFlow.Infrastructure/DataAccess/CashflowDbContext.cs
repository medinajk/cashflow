using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess
{
    internal class CashflowDbContext : DbContext
    {
        // Acesso à tabela Expenses no banco de dados
        public DbSet<Expense> Expenses { get; set; }

        // Configuração e conexão com o banco de dados MySQL
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=localhost;Database=cashflowdb;Uid=root;Pwd=@Password123;";
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 35));

            optionsBuilder.UseMySql(connectionString, serverVersion);
        }
    }
}
