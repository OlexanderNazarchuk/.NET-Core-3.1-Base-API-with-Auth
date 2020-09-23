using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyAuth.Models.AuthModels
{

    public class RegisterModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int OrganizationId { get; set; }
    }
}
