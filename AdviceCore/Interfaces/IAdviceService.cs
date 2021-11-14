using AdviceCore.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdviceCore.Interfaces
{
    public interface IAdviceService
    {
        Task<GetAdviceDTO> GetAdviceAsync();
        Task<IEnumerable<GetAdviceDTO>> GetMultipleAdviceAsync(int number);
    }
}
