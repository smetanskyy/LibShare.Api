namespace LibShare.Api.Data.Interfaces
{
    public interface IRecaptchaService
    {
        bool IsValid(string recaptchaToken);
    }
}
