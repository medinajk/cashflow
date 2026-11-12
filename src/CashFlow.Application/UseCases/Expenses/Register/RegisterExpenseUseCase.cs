using AutoMapper;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Request;
using CashFlow.Communication.Response;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCase.Expenses.Register
{
    public class RegisterExpenseUseCase : IRegisterExpenseUseCase
    {
        private readonly IExpensesRepository _repository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IMapper _mapper;

        public RegisterExpenseUseCase(IExpensesRepository repository, IUnityOfWork unityOfWork, IMapper mapper)
        {
            _repository = repository;
            _unityOfWork = unityOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseRegisterExpenseJson> Execute(RequestRegisterExpenseJson request)
        {
            Validate(request);

            var entity = _mapper.Map<Expense>(request);

            await _repository.Add(entity);

            await _unityOfWork.Commit();

            return _mapper.Map<ResponseRegisterExpenseJson>(entity);
        }
        private void Validate(RequestRegisterExpenseJson request)
        {
            var validator = new RegisterExpenseValidator(); // instancia do validator

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                // seleciona na minha lista de erros, somente as mensagens de erro, e converte para uma lista
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList(); // usamos linq (um conjunto de recursos que estende as capacidades de consulta SQL para as linguagens C#, possibilitando consultas com filtros como where, select)

                throw new ErrorOnValidationException(errorMessages);
            
            }
         }
    }
}