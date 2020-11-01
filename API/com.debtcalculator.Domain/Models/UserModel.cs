namespace com.debtcalculator.Domain.Models
{
    public class UserModel
    {
        public UserModel(long id, string name, string email, long idProfile)
        {
            Id = id;
            Name = name;
            Email = email;
            IdProfile = idProfile;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long IdProfile { get; set; }
    }
}