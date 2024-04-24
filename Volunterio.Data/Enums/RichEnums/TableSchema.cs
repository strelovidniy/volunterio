using RichEnum;

namespace Volunterio.Data.Enums.RichEnums;

public class TableSchema : RichEnum<string>
{
    public static TableSchema Dbo = new("dbo");

    public static TableSchema Sportmonks = new("sportmonks");

    private TableSchema(string value) : base(value)
    {
    }
}