using sun.Infrastructure.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace sun.Core.Domains
{
    public static class AsyncTaskExtensions
    {
        public static T GetData<T>(this AsyncTask task)
        {
            return JsonSerializer.Deserialize<T>(task.Data, JsonOptions.Default);
        }
    }
}
