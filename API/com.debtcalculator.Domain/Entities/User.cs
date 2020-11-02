using System;
using com.debtcalculator.Domain.Enums;
using com.debtcalculator.Domain.Helpers;

namespace com.debtcalculator.Domain.Entities
{
    public class User : Entity
    {
        protected User() { }

        public User(string name, string email, string cpf, int idProfile, string password)
        {
            Name = name;
            Email = email;
            CPF = cpf;
            IdProfile = idProfile;
            Salt = StringHelper.GenerateRandomSalt();
            Password = password.Encrypt(Salt);
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public int IdProfile { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string ChangePasswordCode { get; set; }
        public DateTime CodeExpiration { get; set; }


        public void Update(string name, string email)
        {
            Email = email;
            Name = name;
        }

        public void UpdatePassword(string newPassword)
        {
            Salt = StringHelper.GenerateRandomSalt();
            ChangePasswordCode = null;
            Password = newPassword.Encrypt(Salt);
        }

        public void UpdateChangePasswordCode(string changePasswordCode)
        {
            CodeExpiration = DateTime.UtcNow.AddMinutes(10);
            ChangePasswordCode = changePasswordCode.Encrypt(Salt);
        }

        public void TurnIntoAdmin()
        {
            IdProfile = (int)UserProfile.Admin;
        }
        public void TurnIntoUser()
        {
            IdProfile = (int)UserProfile.User;
        }
    }
}