using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BindingDomainSpecificValueTypes;

public class SingleValueModelBinderProvider : IModelBinderProvider
{
    private readonly HashSet<Type> _supportedTypes;

    public SingleValueModelBinderProvider(IEnumerable<Type> supportedTypes)
    {
        _supportedTypes = supportedTypes.ToHashSet();
    }

    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (_supportedTypes.Contains(context.Metadata.ModelType))
        {
            // instances of SingleValueModelBinder could be reused
            return new SingleValueModelBinder(context.Metadata.ModelType);
        }

        return null;
    }
}