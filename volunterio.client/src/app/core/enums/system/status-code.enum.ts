enum StatusCode {
    methodNotAvailable = 0,
    unauthorized = 1,
    forbidden = 2,

    userNotFound = 101,
    roleNotFound = 102,
    helperRoleNotFound = 103,
    userRoleNotFound = 104,
    directoryNotFound = 105,
    fileNotFound = 106,

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
    addressLine1TooLong = 705,
    addressLine2TooLong = 706,
    cityTooLong = 707,
    stateTooLong = 708,
    postalCodeTooLong = 709,
    countryTooLong = 710,

    incorrectPassword = 801,
    passwordsDoNotMatch = 802,
    oldPasswordIncorrect = 803,

    userAlreadyExists = 901,
    roleAlreadyExists = 902,
    emailAlreadyExists = 903,

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
    addressLine1Required = 1012,
    cityRequired = 1013,
    stateRequired = 1014,
    postalCodeRequired = 1015,
    countryRequired = 1016,

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
    lastNameShouldNotContainWhiteSpace = 1402
}

export default StatusCode;
