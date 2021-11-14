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
        private readonly IConfiguration _config;

        public AdviceService(IHttpClientWrapper httpClientWrapper, IConfiguration config)
        {
            _httpClientWrapper = httpClientWrapper;
            _config = config;
        }

        public async Task<GetAdviceDTO> GetAdviceAsync()
        {
            var uri = _config.GetValue<string>("AdviceUrl");

            var result = await _httpClientWrapper.GetAsync<GetAdviceDTO>(uri);

            return result;
        }
    }
}
