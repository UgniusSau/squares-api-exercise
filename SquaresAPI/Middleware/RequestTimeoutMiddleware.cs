namespace SquaresAPI.Middleware
{
    public class RequestTimeoutMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly int _timeoutInSeconds;

        public RequestTimeoutMiddleware(RequestDelegate next, int timeoutInSeconds)
        {
            _next = next;
            _timeoutInSeconds = timeoutInSeconds;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (var cts = new CancellationTokenSource())
            {
                cts.CancelAfter(_timeoutInSeconds * 1000);
                var originalRequestAborted = context.RequestAborted;
                context.RequestAborted = CancellationTokenSource.CreateLinkedTokenSource(originalRequestAborted, cts.Token).Token;

                try
                {
                    await _next(context);
                }
                catch (TaskCanceledException)
                {
                    if (cts.Token.IsCancellationRequested)
                    {
                        context.Response.StatusCode = StatusCodes.Status408RequestTimeout;
                        context.Response.ContentType = "text/plain";
                        await context.Response.WriteAsync("Request timed out.");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }
    }
}
