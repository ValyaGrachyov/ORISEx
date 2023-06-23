using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using Example1;
using Example1.IServices;
using Example1.Services;
using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IService,ScopeService>();
builder.Services.AddSingleton<IService,SingletonService>();
builder.Services.AddTransient<IService,TransientService>();


builder.Services.AddRazorPages();

var app = builder.Build();

var exception = "";


app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapRazorPages();
app.UseMiddleware<CustomMiddleware>();



app.Map("/test",app => app.Run(async context =>
{
    try
    {
        throw new Exception("smth");
    }
    catch (Exception e)
    {
        context.Items["Exception"] = e.Message;
        exception = e.Message;  
    }
}));

app.Map("/Error",() => exception);



app.Map("/1", () =>
{
    var serviceCollection= new ServiceCollection();
    serviceCollection.AddScoped<ScopeService>();
    var serviceProvider= serviceCollection.BuildServiceProvider();
    var t = serviceProvider.GetService(typeof(ScopeService));
});


app.Run();





//Позволяет отобразить пользователю если у него будет какая-нибудь ошибка
//app.UseStatusCodePages("text/plain", "Error. Status code: {0}");
// То же самое, только делается Redirect по пути
//app.UseStatusCodePagesWithRedirects("/StatusCode/{0}");

/*
//Примерчик с работой с контекстом
app.Run(async context =>
{
    context.Response.Headers.Append("Test", "value");
    await context.Response.WriteAsync("Hello world");
});
*/


/*
 app.Map("/", Main);
 
 static void Main(IApplicationBuilder app)
 {
    app.Run(async context =>3
    {
        await context.Response.WriteAsync("Smth");
    });
 }
 
 
 * //Работа UseWhen, Use, Run (Для Map логика такая же)
//Проверяется условие, затем вызывается функция
app.UseWhen(context =>
{
    return context.Request.IsHttps == true;
},Foo);

static void Foo(IApplicationBuilder app)
{
    //Вызывает Run в этом методе
    app.Use(async (context, next) =>
    {
        await next.Invoke();
        await context.Response.WriteAsync("Use");
    });
    
    app.Run(async (context) =>
    {
        await context.Response.WriteAsync("Foo");
    });
    
}

//Если условие плохое, то вызывается этот сегмент
app.Run(async context =>
{
    await context.Response.WriteAsync("Not Found");
});
 */

// //паттерн Options
// var top = new TestOptions();
//
// builder.Configuration.GetSection(TestOptions.Test).Bind(top);
//
// Console.Write(top.Value2);
// Console.WriteLine(top.Value1.Value1_1);
// public class TestOptions
// {
//     public const string Test = "Test";
//     
//     public Value1 Value1 { get; set; }
//     public string Value2 { get; set; }
// }
//
// public class Value1
// {
//     public string Value1_1 { get; set; }
// }





//Брать инфу с appsettings
//Console.WriteLine(builder.Configuration["A:B:C"]);