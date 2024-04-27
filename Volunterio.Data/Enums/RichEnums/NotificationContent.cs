using RichEnum;

namespace Volunterio.Data.Enums.RichEnums;

public class NotificationContent(string value) : RichEnum<string>(value)
{
    public static NotificationContent Test => new("Test");
}