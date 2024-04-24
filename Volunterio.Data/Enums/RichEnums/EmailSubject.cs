using RichEnum;

namespace Volunterio.Data.Enums.RichEnums;

public class EmailSubject : RichEnum<string>
{
    public static EmailSubject ResetPassword =>
        new("Reset your password");

    public static EmailSubject CreateAccount =>
        new("Create your account");

    public static EmailSubject CompleteRegistration =>
        new("Complete Registration");

    private EmailSubject(string value) : base(value)
    {
    }
}