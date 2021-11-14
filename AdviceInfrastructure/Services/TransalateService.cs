using AdviceCore.DTOs;
using AdviceCore.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdviceInfrastructure.Services
{
    public class TransalateService : ITranslateService
    {
        private readonly IHttpClientWrapper _httpClientWrapper;
        private readonly IConfiguration _config;

        public TransalateService(IHttpClientWrapper httpClientWrapper, IConfiguration config)
        {
            _httpClientWrapper = httpClientWrapper;
            _config = config;

        }


        public async Task<IEnumerable<TranslatedAdviceDTO>> TranslateMultipleAdvice(IEnumerable<GetAdviceDTO> advices)
        {
            var fromLanguage = _config.GetValue<string>("TranslateProperty:FromLanguage");
            var toLanguage = _config.GetValue<string>("TranslateProperty:ToLanguage");
            var tasks = new List<Task<TranslatedAdviceDTO>>();

            foreach(var advice in advices)
            {
                var translateObject = new TranslateObjDTO { q = advice?.Slip?.Advice, source = fromLanguage, target = toLanguage };
                tasks.Add(GetTranslatedText(advice, translateObject));
            }

            //Wait for the tasks to complete and return
            return await Task.WhenAll(tasks);
        }

        private async Task<TranslatedAdviceDTO> GetTranslatedText(GetAdviceDTO advice, TranslateObjDTO translateObj)
        {
            var transalateUri = $"{_config.GetValue<string>("TranslateProperty:Url")}";
            var translatedObj = await _httpClientWrapper.PostAsync<TranslatedTextDTO, TranslateObjDTO>(transalateUri, translateObj);

            if (translatedObj != null)
            {
                return new TranslatedAdviceDTO
                {
                    Id = (int)advice?.Slip?.Id,
                    Advice = new Advice
                    {
                        English = advice?.Slip?.Advice,
                        Polish = translatedObj.TranslatedText
                    }

                };
            }
            else
            {
                return default;
            }
        }
    }
}
