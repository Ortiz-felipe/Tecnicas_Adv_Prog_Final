using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace VTVApp.Api.Errors
{
    public class ExtendedProblemDetails : ProblemDetails
    {
        [JsonPropertyName("errors")]
        public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>(StringComparer.Ordinal);

        public ExtendedProblemDetails()
        {
            
        }

        public ExtendedProblemDetails(ApiError apiError)
        {
            if (apiError is null)
            {
                throw new ArgumentNullException("apiError", FormattableString.Invariant(FormattableStringFactory.Create("{0} is null", "apiError")));
            }

            Errors.Add("Error", new string[1] { apiError.Message });
        }
    }
}
