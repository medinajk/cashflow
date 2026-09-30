using CashFlow.Application.UseCase.Expenses.Register;
using CashFlow.Communication.Enum;
using CashFlow.Communication.Request;
using CashFlow.Exception;
using CommandTestUtilities.Requests;
using FluentAssertions;

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
            var request = RequestRegisterExpenseJsonBuilder.Build();

            // act - chama o método de validação
            var result = validator.Validate(request);

            // assert - verifica se o resultado é válido
            // Verificar documentação do FluentAssertions
            result.IsValid.Should().BeTrue();
        }
        [Fact]
        public void Error_Title_Empty()
        {
            // Arrange - instancia o validator
            var validator = new RegisterExpenseValidator();
            var request = RequestRegisterExpenseJsonBuilder.Build();
            request.Title = string.Empty;

            // act - chama o método de validação
            var result = validator.Validate(request);

            // assert - verifica se o resultado é válido
            // Verificar documentação do FluentAssertions
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED));
        }

        [Fact]
        public void Error_Date_Future()
        {
            // Arrange - instancia o validator
            var validator = new RegisterExpenseValidator();
            var request = RequestRegisterExpenseJsonBuilder.Build();
            request.Date = DateTime.UtcNow.AddDays(1);

            // act - chama o método de validação
            var result = validator.Validate(request);

            // assert - verifica se o resultado é válido
            // Verificar documentação do FluentAssertions
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EXPENSE_CANNOT_BE_FOR_THE_FUTURE));
        }

        [Fact]
        public void Error_Payment_Type_Invalid()
        {
            // Arrange - instancia o validator
            var validator = new RegisterExpenseValidator();
            var request = RequestRegisterExpenseJsonBuilder.Build();
            request.PaymentType = (PaymentType)700;

            // act - chama o método de validação
            var result = validator.Validate(request);

            // assert - verifica se o resultado é válido
            // Verificar documentação do FluentAssertions
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_TYPE_INVALID));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Error_Amount_Invalid(decimal amount)
        {
            // Arrange - instancia o validator
            var validator = new RegisterExpenseValidator();
            var request = RequestRegisterExpenseJsonBuilder.Build();
            request.Amount = amount;

            // act - chama o método de validação
            var result = validator.Validate(request);

            // assert - verifica se o resultado é válido
            // Verificar documentação do FluentAssertions
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO));
        }
    }
}
