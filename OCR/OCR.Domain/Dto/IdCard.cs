using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCR.Domain.Dto
{
    public class IdCard
    {
        public string? Number { get; set; }
        public string? FirstNames { get; set; }
        public string? LastNames { get; set; }
        public string? DateOfBirth { get; set; }
    }
}
