namespace Volunterio.Data.Enums;

public enum StatusCode
{
    MethodNotAvailable = 0,
    Unauthorized = 1,
    Forbidden = 2,

    UserNotFound = 101,
    RoleNotFound = 102,
    HelperRoleNotFound = 103,
    UserRoleNotFound = 104,
    DirectoryNotFound = 105,
    FileNotFound = 106,

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
    AddressLine1TooLong = 705,
    AddressLine2TooLong = 706,
    CityTooLong = 707,
    StateTooLong = 708,
    PostalCodeTooLong = 709,
    CountryTooLong = 710,

    IncorrectPassword = 801,
    PasswordsDoNotMatch = 802,
    OldPasswordIncorrect = 803,

    UserAlreadyExists = 901,
    RoleAlreadyExists = 902,
    EmailAlreadyExists = 903,

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
    AddressLine1Required = 1012,
    CityRequired = 1013,
    StateRequired = 1014,
    PostalCodeRequired = 1015,
    CountryRequired = 1016,

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
    LastNameShouldNotContainWhiteSpace = 1402
}