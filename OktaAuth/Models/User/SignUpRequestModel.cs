namespace OktaAuth.Models.User
{
    public class SignUpRequestModel
    {
        public ProfileRequestModel Profile { get; set; }
        public CredentialBaseModel Credentials { get; set; }
    }
}
