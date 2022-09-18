using System.Text.Json;

namespace DogBreedsAPI.Logs
{
    public class CustomLogs
    {

        const string PUT = "put";
        const string PATCH = "patch";
        const string DELETE = "delete";
        const string POST = "post";

        public static void SaveLog200(int id, string message, string dogType, string method, object? entityBefore = null, object? entityAfter = null)
        {
            var now = DateTime.Now.ToString("G");

            if (method.Equals(PUT, StringComparison.InvariantCultureIgnoreCase) || method.Equals(PATCH, StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine($"{now} - {message} id: : {id} - {dogType} - Alterado de {JsonSerializer.Serialize(entityBefore)} para {JsonSerializer.Serialize(entityAfter)}");
            }
            else if (method.Equals(DELETE, StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine($"{now} - {message} id: {id} - {dogType} - Removido");
            }
        }

        public static void SaveLog201(string message, string method)
        {
            var now = DateTime.Now.ToString("G");

            if (method.Equals(PUT, StringComparison.InvariantCultureIgnoreCase) || method.Equals(POST, StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine($"{now} - {message} - Criado");
            }
        }

    }
}
