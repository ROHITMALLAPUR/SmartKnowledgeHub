namespace SmartKnowledgeHub.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {

        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {


            try
            {
                await _next(context);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsJsonAsync(new
                {
                    statusCode = 500,
                    message = "An unexpected error occurred.",

                });
            }
        }
    }
}
