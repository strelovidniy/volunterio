using RichEnum;

namespace Volunterio.Data.Enums.RichEnums;

public class TableName(string value) : RichEnum<string>(value)
{
    public static TableName Addresses => new("Addresses");

    public static TableName Roles => new("Roles");

    public static TableName UserDetails => new("UserDetails");

    public static TableName Users => new("Users");
}