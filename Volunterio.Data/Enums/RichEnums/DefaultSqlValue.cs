using RichEnum;

namespace Volunterio.Data.Enums.RichEnums;

public class DefaultSqlValue : RichEnum<string>
{
    public static DefaultSqlValue NewGuid => new("uuid_generate_v4()");

    public static DefaultSqlValue NowUtc => new("now() at time zone('utc')");

    private DefaultSqlValue(string value) : base(value)
    {
    }
}