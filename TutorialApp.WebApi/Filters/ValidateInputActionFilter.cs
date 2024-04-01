using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TutorialApp.Business.Common.Constants;

namespace TutorialApp.WebApi.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidateInputActionFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ValidationFailedResult(context.ModelState);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ValidationFailedResult : ObjectResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelState"></param>
        public ValidationFailedResult(ModelStateDictionary modelState)
            : base(new ValidationResultModel(modelState))
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity; //change the http status code to 422.
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CustomValidationFailedResult : ObjectResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelState"></param>
        public CustomValidationFailedResult(ModelStateDictionary modelState)
            : base(new CustomValidationResultModel(modelState))
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity; //change the http status code to 422.
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CustomValidationResultModel
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ValidationError> Errors { get; } = new List<ValidationError>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelState"></param>
        public CustomValidationResultModel(ModelStateDictionary modelState)
        {
            foreach (var key in modelState.Keys)
            {
                var modelErrorCollection = modelState[key]?.Errors;
                if (modelErrorCollection != null)
                    foreach (var subError in modelErrorCollection)
                    {
                        if (int.TryParse(subError.ErrorMessage, out var messageCode))
                        {
                            var errorMessage = MessageExtensions.GetMessage(messageCode);
                            Errors.Add(new ValidationError(key, messageCode, string.Format(errorMessage)));
                        }
                        else
                        {
                            Errors.Add(new ValidationError(key, 0, subError.ErrorMessage));
                        }
                    }
            }
            modelState.Clear();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ValidationResultModel
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ValidationError> Errors { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelState"></param>
        public ValidationResultModel(ModelStateDictionary modelState)
        {
            Errors = modelState.Keys
                .SelectMany(key => modelState[key]?.Errors.Select(x => new ValidationError(key, 0, x.ErrorMessage))!)
                .Concat(modelState.Keys
                    .SelectMany(key => modelState[key]
                        ?.Errors
                        .Select(x => x.Exception)
                        .OfType<CustomValidationResult>()
                        .SelectMany(result =>
                            result.ValidationErrors.Select(error =>
                                new ValidationError(key, error.Code, error.Message)))!))
                .ToList();

            modelState.Clear();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CustomModelError : ModelError
    {
        /// <summary>
        /// 
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="code"></param>
        public CustomModelError(string errorMessage, int code) : base(errorMessage)
        {
            Code = code;
        }
    }
}