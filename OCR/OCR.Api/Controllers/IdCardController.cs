using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OCR.Application.IService;

namespace OCR.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdCardController : ControllerBase
    {
        private readonly IIdCardService _cedulaService;

        public IdCardController(IIdCardService cedulaService)
        {
            _cedulaService = cedulaService;
        }

        [HttpPost("AnalizarDocumento")]
        public async Task<IActionResult> AnalizarDocumento(IFormFile file)
        {            
            var resultado = await _cedulaService.AnalyzeDocument(file);
            return Ok(resultado);
        }

    }
}
