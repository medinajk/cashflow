using CashFlow.Communication.Response;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace CashFlow.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter // nao é uma herança, mas sim uma implementação de uma interface para criar um filtro de exceção personalizado
    {

        void IExceptionFilter.OnException(ExceptionContext context)
        {
            if (context.Exception is CashFlowException) // toda classe que herda CashFlowException vai cair aqui, ou seja, todas as exceções personalizadas do CashFlow
            {
                HandleProjectException(context);

            }
            else
            {
                ThrowUnknowError(context);
            }
        }

        // funções auxiliares para lidar com as exceções
        private void HandleProjectException(ExceptionContext context)
        {
            if (context.Exception is ErrorOnValidationException ex)
            {
                var errorResponse = new ResponseErrorJson(ex.Errors);

                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new BadRequestObjectResult(errorResponse); // retorna para o usuário uma resposta com o erro em json

            }
            else
            {
                var errorResponse = new ResponseErrorJson(context.Exception.Message); // retorna a mensagem de erro da exceção para o usuário
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Result = new ObjectResult(errorResponse);

            }
        }
        private void ThrowUnknowError(ExceptionContext context)
        {
            var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UNKNOWN_ERROR);
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(errorResponse); // retorna para o usuário uma resposta com o erro em json

        }

    }
}
