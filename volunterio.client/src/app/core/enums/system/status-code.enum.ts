enum StatusCode {
    methodNotAvailable = 0,
    unauthorized = 1,
    forbidden = 2,

    userNotFound = 101,
    roleNotFound = 102,
    helperRoleNotFound = 111,
    userRoleNotFound = 112,

    queryResultError = 201,

    emailSendingError = 301,

    userHasNoRole = 401,

    fileHasAnUnacceptableFormat = 501,

    roleCannotBeUpdated = 601,
    roleCannotBeDeleted = 602,
    userRoleCannotBeChanged = 603,

    firstNameTooLong = 701,
    lastNameTooLong = 702,
    fileIsTooLarge = 703,
    emailTooLong = 704,

    roleTypeRequired = 1001,
    roleIdRequired = 1002,
    userIdRequired = 1003,
    lastNameRequired = 1004,
    firstNameRequired = 1005,
    invitationTokenRequired = 1006,
    passwordRequired = 1007,
    emailRequired = 1008,
    roleNameRequired = 1009,
    verificationCodeRequired = 1010,
    confirmPasswordRequired = 1011,

    passwordLengthExceeded = 1101,

    passwordMustHaveAtLeast8Characters = 1201,
    passwordMustHaveNotMoreThan32Characters = 1202,
    passwordMustHaveAtLeastOneUppercaseLetter = 1203,
    passwordMustHaveAtLeastOneLowercaseLetter = 1204,
    passwordMustHaveAtLeastOneDigit = 1205,

    invalidRoleType = 1301,
    invalidSortByProperty = 1302,
    invalidExpandProperty = 1303,
    invalidEmailFormat = 1304,
    invalidEmailModel = 1305,
    invalidVerificationCode = 1306,
    invalidFile = 1307,

    firstNameShouldNotContainWhiteSpace = 1401,
    lastNameShouldNotContainWhiteSpace = 1402,

    incorrectPassword = 1501,
    passwordsDoNotMatch = 1502,
    oldPasswordIncorrect = 1503,

    userAlreadyExists = 1601,
    roleAlreadyExists = 1602,
    emailAlreadyExists = 1603
}

export default StatusCode;
