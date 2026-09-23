using CashFlow.Application.UseCase.Expenses.Register;
using CashFlow.Communication.Request;

namespace Validators.Tests.Expenses.Register
{
    public class RegisterExpenseValidatorTests
    {
        [Fact] // precisa desse atributo pra identificar que essa função é uma função de teste de unidade
        public void Success()
        {
            // precisam dos 3 As para criar o cenário de teste: Arrange, Act e Assert

            // Arrange - instancia o validator
            var validator = new RegisterExpenseValidator();
            var request = new RequestRegisterExpenseJson
            {
                Amount = 100,
                Date = DateTime.Now.AddDays(-1),
                Description = "Teste",
                PaymentType = CashFlow.Communication.Enum.PaymentType.Cash
            };

            // act - chama o método de validação
            var result = validator.Validate(request);

            // assert - verifica se o resultado é válido
            Assert.True(result.IsValid);
        }
    }
}
