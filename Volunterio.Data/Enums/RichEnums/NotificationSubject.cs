using RichEnum;

namespace Volunterio.Data.Enums.RichEnums;

public class NotificationSubject(string value) : RichEnum<string>(value)
{
    public static NotificationSubject Test => new("Test");
}