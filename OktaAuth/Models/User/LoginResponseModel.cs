using System;

namespace OktaAuth.Models.User
{
    public class LoginResponseModel
    {
        public DateTime ExpiresAt { get; set; }
        public string Status { get; set; }
        public string SessionToken { get; set; }
        public Embedded _embedded  { get; set; }
    }

    public class Embedded
    {
        public UserModel User { get; set; }
    }
}
