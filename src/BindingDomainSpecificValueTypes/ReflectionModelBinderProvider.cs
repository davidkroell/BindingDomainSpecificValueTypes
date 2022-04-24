using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BindingDomainSpecificValueTypes;

public class ReflectionModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (TypeHasSingleCtorWithSingleParameter(context.Metadata.ModelType))
        {
            return new SingleValueModelBinder(context.Metadata.ModelType);
        }

        return null;
    }

    private static bool TypeHasSingleCtorWithSingleParameter(Type type)
    {
        var ctors = type.GetConstructors();

        if (ctors.Length == 1)
        {
            return ctors.First().GetParameters().Length == 1;
        }

        return false;
    }
}
