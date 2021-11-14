using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdviceCore.Interfaces
{
    public interface IHttpClientWrapper
    {
        Task<T> GetAsync<T>(string uri);
    }
}
