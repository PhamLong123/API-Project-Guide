namespace CSService.Models
{
    public enum Status
    {
        Success = 0,
        CannotSaveRequest = 1,
        CannotSaveResponse = 2,
        IPNotAccess = 3,
        AuthenticateNotAccess = 4,
        SourceNotExists = 5,
        ProviderNotConfig = 6,
        MethodNotInput = 7,
        ProviderNotInput = 8,
        ProviderInvalid = 9,
        EmailNotInput = 10,
        PhoneNotInput = 11,
        AppIDNotInput = 12,
        SubjectNotInput = 13,
        ContentNotInput = 14,
        SubjectNotExists = 15,
        ContentNotExists = 16,
        DeviceNotConfig = 17,
        DeviceNotInput = 18,
        SendFail = 19,
        PaymentNotConfig = 20,
        PaymentNotInput = 21,
        PaymentResultNotConfig = 22,
        PaymentResultNotInput = 23,
        PaymentStatusNotConfig = 24,
        PaymentStatusNotInput = 25,
        PurchaseFail = 26,
        PurchasePending = 27,
        APIKeyInvalid = 28,
        ErrorConfig = 29,
        ErrorExecute = 30,
        ErrorException = 31,
        WarnningExecute = 32,
        AuthenticationFailure = 100,
        AuthenticationIsDeactive = 101,
        AuthenticationIsExpired = 102,
        AuthenticationIsLocked = 103,
        TemplateNotActived = 104,
        TemplateDoesNotExisted = 105,
        PhoneNumberInBlackList = 108,
        SendTheSameContentInShortTime = 304,
        NotEnoughMemory = 400,
        GatewayProviderError = 900,
        LengthOfContent = 901,
        MsisdnMustBeGreaterThanZero = 902,
        BrandNameInActive = 904,
        ErrCodeHasNotBeenIdentified = 9999,
        HttpRequestError = 10000
    }

    public class Result
    {
        public Status Code { get; set; }
        public string Message { get; set; }
    }
}
