using RichEnum;

namespace Volunterio.Data.Enums.RichEnums;

public class FileExtension(string value) : RichEnum<string>(value)
{
    public static FileExtension Png => new("png");
}