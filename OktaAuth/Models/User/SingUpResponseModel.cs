using System;
using System.Collections.Generic;

namespace OktaAuth.Models.User
{
    public class SignUpResponseModel
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Activated { get; set; }
        public DateTime StatusChanged { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime PasswordChanged { get; set; }
        public TypeModel Type { get; set; }
        public ProfileModel Profile { get; set; }
        public CredentialModel Credentials { get; set; }
    }

    public class TypeModel
    {
        public string Id { get; set; }
    }

    public class ProfileModel: ProfileBaseModel
    {
        public string MobilePhone { get; set; }
        public string SecondEmail { get; set; }
        public string Email { get; set; }
    }

    public class CredentialModel: CredentialBaseModel
    {
        public List<EmailModel> Emails { get; set; }
        public ProviderModel Provider { get; set; }
    }
    public class EmailModel
    {
        public string Value { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }

    public class ProviderModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }


}
