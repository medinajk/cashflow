namespace CashFlow.Communication.Response
{
    public class ResponseErrorJson
    {
        public List<string> ErrorMessages { get; set; }


        // recebem o mesmo nome mas os parametros sao diferentes, entao nao importa
        public ResponseErrorJson(string errorMessage)
        {
            ErrorMessages = new List<string> { errorMessage } ; // adiciona o erro recebido na lista de erros
        }

        public ResponseErrorJson(List<string> errorMessage)
        {
            ErrorMessages = errorMessage; // recebe a lista de erros e atribui a propriedade ErrorMessages
        }
    }
}
