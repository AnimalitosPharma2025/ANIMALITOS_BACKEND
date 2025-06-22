namespace ANIMALITOS_PHARMA_API.Controllers.Helpers
{
    internal class ErrorHandler
    {
        public static ResultHelper GetErrorResult(Exception ex)
        {
            ResultHelper resultHelper = new ResultHelper();
            resultHelper.IsSuccess = false;
            resultHelper.Message = ex.Message;

            return resultHelper;
        }
    }
}
