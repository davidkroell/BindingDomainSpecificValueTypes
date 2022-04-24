using System.ComponentModel;

namespace BindingDomainSpecificValueTypes;

[TypeConverter(typeof(ArticleNumberConverter))]
public record ArticleNumber
{
    public string Value { get; }

    public ArticleNumber(string articleNumber)
    {
        if (articleNumber.Length != 9)
        {
            throw new ArgumentException("Must have a length of 9");
        }

        if (!articleNumber.All(char.IsDigit))
        {
            throw new FormatException("Must be all digits");
        }

        Value = articleNumber;
    }
}
