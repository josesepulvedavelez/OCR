using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OCR.Api.Common;
using OCR.Application.IService;
using OCR.Domain.Dto;
using System.Reflection.Metadata;

namespace OCR.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdCardController : ControllerBase
    {
        private readonly IIdCardService _iIdCardService;

        public IdCardController(IIdCardService iIdCardService)
        {
            _iIdCardService = iIdCardService;
        }

        /// <summary>
        /// Analyze a Colombian ID card and extract relevant information.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("AnalyzeDocument")]
        public async Task<ActionResult<ApiResult<IdCard>>> AnalyzeDocument(IFormFile file)
        {
            var result = await _iIdCardService.AnalyzeDocument(file);
            return ApiResult<IdCard>.Ok(result);
        }

    }
}
