using System.Globalization;

namespace CashFlow.Api.Middleware
{
    public class CultureMiddleware
    {
        // RequestDelegate é uma permissão: se pode ou não continuar o fluxo da requisição
        private readonly RequestDelegate _next;
        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context) // declaração padrão
        {
            // faz uma lista de todas as linguagens suportadas pelo .net
            var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

            // extrai do header da requisição a linguagem desejada pelo cliente
            var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

            // linguagem padrão caso o cliente não escolha uma linguagem específica
            var cultureInfo = new CultureInfo("en");

            // se o cliente escolher uma linguagem específica, a cultura é configurada para essa linguagem
            if (string.IsNullOrEmpty(requestedCulture) == false && supportedLanguages.Any(culture => culture.Name.Equals(requestedCulture)))
            {
                cultureInfo = new CultureInfo(requestedCulture);
            }

            // altera a api para devolver a cultura correta para o cliente
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            await _next(context); // permite o fluxo continuar

        }

    }
}
