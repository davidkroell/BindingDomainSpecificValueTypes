using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;

namespace BindingDomainSpecificValueTypes;

[Route("binding")]
public class BindingController : ControllerBase
{
    [Route("int/{i}")]
    public IActionResult GetInt(int i)
    {
        // route does also match if a non-int is provided
        // it's possible to use {i:int} to specify the type which is required,
        // then a HTTP 404 will return instead
        Console.WriteLine($"ModelState.IsValid: {ModelState.IsValid}");
        return GetBindingMessage(i);
    }

    [Route("guid/{g}")]
    public IActionResult GetGuid(Guid g)
    {
        // this happens under the hood
        // inside SimpleTypeModelBinder
        // it resolves the correct Converter via TypeDescriptor.GetConverter()
        // --> new GuidConverter().ConvertFrom(null, CultureInfo.CurrentCulture, g);

        return GetBindingMessage(g);
    }

    [Route("articleNumber/string/{a}")]
    public IActionResult GetWithString(string a)
    {
        var articleNumber = new ArticleNumber(a);

        return GetBindingMessage(articleNumber);
    }

    [Route("articleNumber/typeConverter/{a}")]
    public IActionResult GetWithTypeConverter(ArticleNumber a)
    {
        foreach (var (key, modelState) in ModelState)
        {
            var errorString = modelState.Errors.Aggregate(string.Empty,
                (s, error) => $"{s} Message: '{error.ErrorMessage}', Exception: {error.Exception}\n");

            Console.WriteLine(
                $"Try binding '{modelState.AttemptedValue}' to variable '{key}': modelState is {modelState.ValidationState} errors: \n{errorString}");
        }

        return GetBindingMessage(a);
    }

    [Route("articleNumber/modelBinder/{a}")]
    public IActionResult GetWithModelBinder(
        [ModelBinder(typeof(SingleValueModelBinder<ArticleNumber>))]
        ArticleNumber a)
    {
        return GetBindingMessage(a);
    }

    [Route("articleNumber/modelBindingProvider/{a}")]
    public IActionResult GetWithModelBindingProvider(ArticleNumber a)
    {
        return GetBindingMessage(a);
    }

    private IActionResult GetBindingMessage<T>(T value, [CallerArgumentExpression("value")] string? name = null)
    {
        return Ok($"Value '{value}' was bound to variable '{name}', type: '{typeof(T)}'");
    }
}
