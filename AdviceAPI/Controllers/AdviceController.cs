using AdviceCore.DTOs;
using AdviceCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdviceAPI.Controllers
{
    public class AdviceController : BaseApiController
    {
        private readonly IAdviceService _adviceService;


        public AdviceController(IAdviceService adviceService)
        {
            _adviceService = adviceService;
        }


        [HttpGet]
        public async Task<ActionResult<GetAdviceDTO>> GetAdvice()
        {
            var advice = await _adviceService.GetAdviceAsync();

            return Ok(advice);
        }

        [HttpGet("{amount}")]
        public async Task<ActionResult<GetAdviceDTO>> GetAdviceByAmount(int amount)
        {
            if (amount >= 5 && amount <= 20)
            {
                var advices = await _adviceService.GetMultipleAdviceAsync(amount);
                return Ok(advices);

            }
            else
            {
                return Ok(new { error = "enter a number betwwen 5 and 20" });
            }
        }
    }
}
