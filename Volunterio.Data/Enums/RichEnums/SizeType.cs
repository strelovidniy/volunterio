using RichEnum;

namespace Volunterio.Data.Enums.RichEnums;

public class SizeType : RichEnum<string>
{
    public static SizeType Bytes => new("B");

    public static SizeType Megabytes => new("MB");

    public static SizeType Kilobytes => new("MB");

    private SizeType(string value) : base(value)
    {
    }
}