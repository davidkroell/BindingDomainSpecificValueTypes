using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BindingDomainSpecificValueTypes;

public class SingleValueModelBinder : IModelBinder
{
    private readonly Type _modelType;

    public SingleValueModelBinder(Type modelType)
    {
        _modelType = modelType;
    }

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var providerResult = bindingContext.ValueProvider.GetValue(bindingContext.FieldName);

        try
        {
            var instace = Activator.CreateInstance(_modelType, providerResult.FirstValue);
            bindingContext.Result = ModelBindingResult.Success(instace);
        }
        catch (TargetInvocationException e)
        {
            // the "real" exception is nested 
            if (e.InnerException != null)
            {
                throw e.InnerException;
            }
        }

        return Task.CompletedTask;
    }
}

public class SingleValueModelBinder<TModel> : SingleValueModelBinder
{
    public SingleValueModelBinder() : base(typeof(TModel))
    {
    }
}
