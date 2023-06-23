using Example1.IServices;
using Example1.Services;

namespace Example1;

public class CustomMiddleware
{
    private readonly RequestDelegate _next;

    public CustomMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context/*, IService _service*/)
    {
        // Console.WriteLine("Hi from Middleware");
        // Console.WriteLine(_service.Guid.ToString());
        // context.Items["Item"] = "customMiddleware";
        
        
        await _next.Invoke(context);
 
        if (context.Items.ContainsKey("Exception"))
        {
            context.Response.Redirect("/Error");
        }
        
    }
}