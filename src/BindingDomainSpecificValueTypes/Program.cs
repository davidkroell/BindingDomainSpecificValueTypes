using BindingDomainSpecificValueTypes;


// build web-app and add all controllers to the DI container
var builder = WebApplication.CreateBuilder(args);

// configure the model binding provider here with the supported types
builder.Services.AddControllers(o =>
{
    // enable custom ModelBinderProviders
    // o.ModelBinderProviders.Insert(0, new SingleValueModelBinderProvider(new [] { typeof(ArticleNumber) }));
    // o.ModelBinderProviders.Insert(0, new ReflectionModelBinderProvider());
});

var app = builder.Build();

// configure middlewares
app.UseDeveloperExceptionPage();
app.MapControllers();

app.Run();
