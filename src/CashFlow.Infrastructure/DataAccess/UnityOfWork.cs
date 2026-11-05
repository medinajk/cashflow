using CashFlow.Domain.Repositories;

namespace CashFlow.Infrastructure.DataAccess
{
    internal class UnityOfWork : IUnityOfWork
    {
        private readonly CashflowDbContext _dbContext;
        public UnityOfWork(CashflowDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Commit() => await _dbContext.SaveChangesAsync();
    }
}
