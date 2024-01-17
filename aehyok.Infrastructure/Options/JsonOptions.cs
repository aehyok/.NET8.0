using aehyok.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace aehyok.Infrastructure.Options
{
    public static class JsonOptions
    {
        private static JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Converters = {
                new JsonLongConverter()
            }
        };

        public static JsonSerializerOptions Default
        {
            get
            {
                return _jsonSerializerOptions;
            }
        }
    }
}
