using System;
using System.Collections.Generic;

namespace OktaAuth.Models.User
{
    public class UserModel
    {
        public string Id { get; set; }
        public DateTime PasswordChanged { get; set; }
        public ProfileResponseModel Profile { get; set; }
    }
    public class ProfileBaseModel
    {
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class ProfileResponseModel : ProfileBaseModel
    {
        public string Locale { get; set; }
        public string TimeZone { get; set; }
    }

    public class ProfileRequestModel : ProfileBaseModel
    {
        public string Email { get; set; }
    }

    public class CredentialBaseModel
    {
        public PasswordModel Password { get; set; }
    }

    public class PasswordModel
    {
        public string Value { get; set; }
    }
}
