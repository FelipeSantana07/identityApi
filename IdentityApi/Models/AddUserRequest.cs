﻿using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityApi.Models
{
    public class AddUserRequest
    {
        public string email { get; set; }

        public string password { get; set; }

        public string rg { get; set; }

    }
}
