using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using VTVApp.Api.Errors;

namespace VTVApp.Api.Extensions
{
    public static class HandlerExtensions
    {
        public static IActionResult
            NoContent<TRequest, TResponse>(this IRequestHandler<TRequest, TResponse> handler)
            where TRequest : IRequest<TResponse> where TResponse : IActionResult
        {
            return new NoContentResult();
        }

        public static IActionResult Ok<TRequest, TResponse>(this IRequestHandler<TRequest, TResponse> handler)
            where TRequest : IRequest<TResponse> where TResponse : IActionResult
        {
            return new OkResult();
        }

        public static IActionResult Ok<TRequest, TResponse>(this IRequestHandler<TRequest, TResponse> handler, object value)
            where TRequest : IRequest<TResponse> where TResponse : IActionResult
        {
            return new OkObjectResult(value);
        }

        public static IActionResult CreatedAtAction<TRequest, TResponse>(
            this IRequestHandler<TRequest, TResponse> handler, string actionName, string controllerName,
            object routeValues, object value) where TRequest : IRequest<TResponse> where TResponse : IActionResult
        {
            return new CreatedAtActionResult(actionName, controllerName, routeValues, value);
        }

        public static IActionResult CreatedAtRoute<TRequest, TResponse>(
            this IRequestHandler<TRequest, TResponse> handler, string routeName, object routeValues, object value)
            where TRequest : IRequest<TResponse> where TResponse : IActionResult
        {
            return new CreatedAtRouteResult(routeName, routeValues, value);
        }

        public static IActionResult NotFound<TRequest, TResponse>(this IRequestHandler<TRequest, TResponse> handler,
            ApiError error) where TRequest : IRequest<TResponse> where TResponse : IActionResult
        {
            return new NotFoundObjectResult(new ExtendedProblemDetails(error));
        }

        public static IActionResult Conflict<TRequest, TResponse>(this IRequestHandler<TRequest, TResponse> handler,
            ApiError error) where TRequest : IRequest<TResponse> where TResponse : IActionResult
        {
            return new ConflictObjectResult(new ExtendedProblemDetails(error));
        }

        public static IActionResult BadRequest<TRequest, TResponse>(this IRequestHandler<TRequest, TResponse> handler,
            ApiError error) where TRequest : IRequest<TResponse> where TResponse : IActionResult
        {
            return new BadRequestObjectResult(new ExtendedProblemDetails(error));
        }

        public static IActionResult BadRequest<TRequest, TResponse>(this IRequestHandler<TRequest, TResponse> handler,
            ModelStateDictionary modelState) where TRequest : IRequest<TResponse> where TResponse : IActionResult
        {
            return new BadRequestObjectResult(modelState);
        }

        public static IActionResult InternalServerError<TRequest, TResponse>(
            this IRequestHandler<TRequest, TResponse> handler, ApiError error) where TRequest : IRequest<TResponse>
            where TResponse : IActionResult
        {
            return new ObjectResult(new ExtendedProblemDetails(error))
                { StatusCode = StatusCodes.Status500InternalServerError };
        }

        public static IActionResult PreconditionFailed<TRequest, TResponse>(
            this IRequestHandler<TRequest, TResponse> handler, ApiError error) where TRequest : IRequest<TResponse>
            where TResponse : IActionResult
        {
            return new ObjectResult(new ExtendedProblemDetails(error))
                { StatusCode = StatusCodes.Status412PreconditionFailed };
        }

        public static IActionResult Unauthorized<TRequest, TResponse>(this IRequestHandler<TRequest, TResponse> handler,
            ApiError error) where TRequest : IRequest<TResponse> where TResponse : IActionResult
        {
            return new ObjectResult(new ExtendedProblemDetails(error))
                { StatusCode = StatusCodes.Status401Unauthorized };
        }

        public static IActionResult Forbidden<TRequest, TResponse>(this IRequestHandler<TRequest, TResponse> handler,
            ApiError error) where TRequest : IRequest<TResponse> where TResponse : IActionResult
        {
            return new ObjectResult(new ExtendedProblemDetails(error))
                { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}
