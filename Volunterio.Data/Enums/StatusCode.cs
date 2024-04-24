namespace Volunterio.Data.Enums;

public enum StatusCode
{
    MethodNotAvailable = 0,
    Unauthorized = 1,
    Forbidden = 2,

    UserNotFound = 101,
    RoleNotFound = 102,
    HelperRoleNotFound = 111,
    UserRoleNotFound = 112,

    QueryResultError = 201,

    EmailSendingError = 301,

    UserHasNoRole = 401,

    FileHasAnUnacceptableFormat = 501,

    RoleCannotBeUpdated = 601,
    RoleCannotBeDeleted = 602,
    UserRoleCannotBeChanged = 603,

    FirstNameTooLong = 701,
    LastNameTooLong = 702,
    FileIsTooLarge = 703,
    EmailTooLong = 704,

    RoleTypeRequired = 1001,
    RoleIdRequired = 1002,
    UserIdRequired = 1003,
    LastNameRequired = 1004,
    FirstNameRequired = 1005,
    InvitationTokenRequired = 1006,
    PasswordRequired = 1007,
    EmailRequired = 1008,
    RoleNameRequired = 1009,
    VerificationCodeRequired = 1010,
    ConfirmPasswordRequired = 1011,

    PasswordLengthExceeded = 1101,

    PasswordMustHaveAtLeast8Characters = 1201,
    PasswordMustHaveNotMoreThan32Characters = 1202,
    PasswordMustHaveAtLeastOneUppercaseLetter = 1203,
    PasswordMustHaveAtLeastOneLowercaseLetter = 1204,
    PasswordMustHaveAtLeastOneDigit = 1205,

    InvalidRoleType = 1301,
    InvalidSortByProperty = 1302,
    InvalidExpandProperty = 1303,
    InvalidEmailFormat = 1304,
    InvalidEmailModel = 1305,
    InvalidVerificationCode = 1306,
    InvalidFile = 1307,

    FirstNameShouldNotContainWhiteSpace = 1401,
    LastNameShouldNotContainWhiteSpace = 1402,

    IncorrectPassword = 1501,
    PasswordsDoNotMatch = 1502,
    OldPasswordIncorrect = 1503,

    UserAlreadyExists = 1601,
    RoleAlreadyExists = 1602,
    EmailAlreadyExists = 1603
}