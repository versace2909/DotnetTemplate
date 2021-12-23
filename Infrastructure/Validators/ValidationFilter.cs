using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Infrastructure.Validators;

public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errorsInModelState = context
                .ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(kvp =>
                        kvp.Key,
                    kvp => kvp.Value.Errors.Select(x => x.ErrorMessage))
                .ToArray();

            List<string> errors = new List<string>();
            foreach (var error in errorsInModelState)
            {
                foreach (var subError in error.Value)
                {
                    errors.Add(subError);
                }
            }

            context.Result = new BadRequestObjectResult(ResponseBaseModel.Fail(errors));
            return;
        }

        await next();
    }
}