using Microsoft.AspNetCore.Http;
using OCR.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCR.Application.IService
{
    public interface IIdCardService
    {
        Task<IdCard> AnalyzeDocument(IFormFile file);
    }
}
