using AdviceCore.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdviceCore.Interfaces
{
    public interface ITranslateService
    {
        Task<IEnumerable<TranslatedAdviceDTO>> TranslateMultipleAdvice(IEnumerable<GetAdviceDTO> advices);
    }
}
