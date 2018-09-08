using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotaHelper.Services.Commons.Interfaces
{
    public interface IHttpClient : IDisposable
    {
        Task<string> GetAsync(string uri, IDictionary<string, string> headers = null);
    }
}
