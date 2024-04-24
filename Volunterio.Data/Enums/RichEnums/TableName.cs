using RichEnum;

namespace Volunterio.Data.Enums.RichEnums;

public class TableName : RichEnum<string>
{
    public static TableName Cities => new("Cities");

    public static TableName Continents => new("Continents");

    public static TableName Countries => new("Countries");

    public static TableName Regions => new("Regions");

    public static TableName Types => new("Types");

    public static TableName Roles => new("Roles");

    public static TableName Users => new("Users");

    private TableName(string value) : base(value)
    {
    }
}