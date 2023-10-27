using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace VTVApp.Api.Errors
{
    public class ApiError
    {
        private readonly string _description;
        private readonly int _majorErrorCode;
        private readonly int _minorErrorCode;
        
        [JsonIgnore]
        public string Message {
            get
            {
                DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(3, 2);
                handler.AppendFormatted(_majorErrorCode + _minorErrorCode);
                handler.AppendLiteral(" - ");
                handler.AppendFormatted(_description);
                return handler.ToStringAndClear();
            }

        }

        public ApiError(int majorErrorCode, int minorErrorCode, string description)
        {
            _description = description;
            _majorErrorCode = majorErrorCode;
            _minorErrorCode = minorErrorCode;
        }
    }
}
