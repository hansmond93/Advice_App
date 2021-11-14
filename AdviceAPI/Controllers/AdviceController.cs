using AdviceCore.DTOs;
using AdviceCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AdviceAPI.Controllers
{
    public class AdviceController : BaseApiController
    {
        private readonly IAdviceService _adviceService;
        private readonly ITranslateService _translateService;


        public AdviceController(IAdviceService adviceService, ITranslateService translateService)
        {
            _translateService = translateService;
            _adviceService = adviceService;
        }


        [HttpGet]
        public async Task<ActionResult<GetAdviceDTO>> GetAdvice()
        {
            var advice = await _adviceService.GetAdviceAsync();

            if (advice == null) return StatusCode(500);

            return Ok(advice);
        }

        [HttpGet("{amount}")]
        public async Task<ActionResult<GetAdviceDTO>> GetAdviceByAmount(int amount)
        {
            if (amount >= 5 && amount <= 20)
            {
                var advices = await _adviceService.GetMultipleAdviceAsync(amount);

                if(advices != null)
                {
                    var translatedAdvices = await _translateService.TranslateMultipleAdvice(advices);
                    translatedAdvices.OrderBy(tA => tA.Id);

                    return Ok(new { translatedAdvices });
                }
                return StatusCode(500);

            }
            else
            {
                return Ok(new { error = "enter a number betwwen 5 and 20" });
            }
        }

    }
}
