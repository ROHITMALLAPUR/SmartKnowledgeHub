using SmartKnowledgeHub.API.DTOs;

namespace SmartKnowledgeHub.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {

        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {


            try
            {
                await _next(context);
            }
            catch (KeyNotFoundException ex)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                var errorResponse = new ErrorResponseDto
                {
                    StatusCode = 404,
                    Message = "Resource not found.",
                };
                await context.Response.WriteAsJsonAsync(errorResponse);
            }
         
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var errorResponse = new ErrorResponseDto
                {
                    StatusCode = 500,
                    Message = "An unexpected error occurred.",
                };

                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}
