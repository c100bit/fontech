namespace FonTech.Domain.Enum;

public enum ErrorCodes
{
    InternalServerError = 10,

    ReportsNotFound = 0,
    ReportNotFound = 1,
    ReportAlreadyExists = 2,

    UserNotFound = 11,
    PasswordIsWrong = 12,

    PaswordNotEqualsPasswordConfirm = 21,
    UserAlreadyExists = 22
}