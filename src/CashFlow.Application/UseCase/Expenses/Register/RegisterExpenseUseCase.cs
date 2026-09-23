using CashFlow.Communication.Enum;
using CashFlow.Communication.Request;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCase.Expenses.Register
{
    public class RegisterExpenseUseCase
    {
        public RequestRegisterExpenseJson Execute(RequestRegisterExpenseJson request)
        {
            ValidateRequest(request);

            return new RequestRegisterExpenseJson();
        }
        private void ValidateRequest(RequestRegisterExpenseJson request)
        {
            var validator = new RegisterExpenseValidator(); // instancia do validador

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