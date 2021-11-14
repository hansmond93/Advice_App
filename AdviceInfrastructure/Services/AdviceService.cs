using AdviceCore.DTOs;
using AdviceCore.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdviceInfrastructure.Services
{
    public class AdviceService : IAdviceService
    {
        private readonly IHttpClientWrapper _httpClientWrapper;
        private readonly string _uri;

        public AdviceService(IHttpClientWrapper httpClientWrapper, IConfiguration config)
        {
            _httpClientWrapper = httpClientWrapper;
            _uri = config.GetValue<string>("AdviceUrl");
        }

        public async Task<GetAdviceDTO> GetAdviceAsync()
        {
            var result = await _httpClientWrapper.GetAsync<GetAdviceDTO>(_uri);

            return result;
        }

        public async Task<IEnumerable<GetAdviceDTO>> GetMultipleAdviceAsync(int number)
        {
            var advices = new GetAdviceDTO[number];

            for(int i=0; i < number; i++)
            {
                var advice = await GetAdviceAsync();
                if(advice != null)
                {
                    // do this because advice API caches advices for 2 seconds
                    // else concurrent request for advices would have probably been the best approach 
                    await Task.Delay(2000);
                    advices[i] = advice;
                }
                else
                {
                    return null;
                }
            }

            return advices;
        }
    }
}
