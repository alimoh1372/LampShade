using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.RoleContracts;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Application.Contracts.AccountContracts
{
    public class CreateAccount
    {
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        public string Fullname { get; set; }

        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        public string Username { get; set; }

        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        public string Password { get; set; }

        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        public string Mobile { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = ValidatingMessage.IsRequired)]
        public long RoleId { get; set; }

        public IFormFile ProfilePhoto { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}