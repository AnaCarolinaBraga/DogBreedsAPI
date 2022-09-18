using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DogBreedsAPI.Utils
{
    public class SwaggerTitleConfig : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var uri = new Uri("https://www.linkedin.com/in/anacarolina-braga/");
            swaggerDoc.Info.Description = "API construída como projeto final do módulo de Programação Web III";
            swaggerDoc.Info.Contact = new OpenApiContact
            {
                Name = "Ana Carolina Martins Braga",
                Email = "anabraga95@gmail.com",
                Url = uri
            };


        }
    }
}
