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
    }
}
