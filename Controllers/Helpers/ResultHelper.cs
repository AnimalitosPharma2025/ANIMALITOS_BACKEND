using System.Collections;

namespace ANIMALITOS_PHARMA_API.Controllers.Helpers
{
    internal class ResultHelper
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public IList? ResultObject { get; set; }
    }
}