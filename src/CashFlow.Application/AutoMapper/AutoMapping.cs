using AutoMapper;
using CashFlow.Communication.Request;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToEntity();
            EntityToResponse();
        }

        // Requisição para Entidade
        private void RequestToEntity()
        {
            CreateMap<RequestRegisterExpenseJson, Expense>();
        }

        // Resposta
        private void EntityToResponse()
        {
            CreateMap<Expense, RequestRegisterExpenseJson>();
        }
    }
}
