using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace VTVApp.Api.Filters
{
    public class DateParameterOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters != null)
            {
                foreach (var parameter in operation.Parameters)
                {
                    if (parameter.Name == "date" && parameter.In == ParameterLocation.Path)
                    {
                        parameter.Schema.Type = "string";
                        parameter.Schema.Format = "date"; // or "date-time" if you expect time as well
                        parameter.Example = new OpenApiString(DateTime.UtcNow.ToString("yyyy-MM-dd"));
                    }
                }
            }
        }
    }
}
