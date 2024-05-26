using System.Net;

namespace VuLongRazorPages.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                // Handle the exception here
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Handles the exception and sets the appropriate status code and response.
        /// </summary>
        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            switch (ex)
            {

            };

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var result = new {message = ex.Message};
            return context.Response.WriteAsJsonAsync(result);
        }
    }
}
