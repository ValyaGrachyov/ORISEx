var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


app.Map("/error/{code}", (int code) => $"{code}");

app.Run();

/*
 app.Map("/", Main);
 
 static void Main(IApplicationBuilder app)
 {
    app.Run(async context =>
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