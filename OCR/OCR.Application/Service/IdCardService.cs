using Azure;
using Azure.AI.DocumentIntelligence;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OCR.Application.IService;
using OCR.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCR.Application.Service
{
    public class IdCardService : IIdCardService
    {
        private readonly DocumentIntelligenceClient _client;             

        public IdCardService(IConfiguration configuration)
        {
            string endpoint = configuration["endpoint3"];
            string key = configuration["key3"];

            _client = new DocumentIntelligenceClient(new Uri(endpoint), new AzureKeyCredential(key));            
        }

        public async Task<IdCard> AnalyzeDocument(IFormFile file)
        {
            MemoryStream memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            BinaryData binaryData = BinaryData.FromBytes(memoryStream.ToArray());

            var analyzeOptions = new AnalyzeDocumentOptions("prebuilt-idDocument", binaryData)
            {
                Features = { DocumentAnalysisFeature.QueryFields },
                QueryFields = { "Nombres", "Apellidos", "Numero" }
            };

            var opertation = await _client.AnalyzeDocumentAsync(WaitUntil.Completed, analyzeOptions);
            var identityDocuments = opertation.Value;
            var identityDocument = identityDocuments.Documents.FirstOrDefault();

            string? documentNumber = identityDocument?.Fields["DocumentNumber"].Content.ToString();
            string? firstNames = identityDocument?.Fields["FirstName"].Content.ToString();
            string? lastNames = identityDocument?.Fields["LastName"].Content.ToString();
            string? dateOfBirth = identityDocument?.Fields["DateOfBirth"].Content.ToString();

            var result = new IdCard
            {
                FirstNames = firstNames,
                LastNames = lastNames,
                Number = documentNumber,
                DateOfBirth = dateOfBirth
            };

            return result;
        }

    }
}
