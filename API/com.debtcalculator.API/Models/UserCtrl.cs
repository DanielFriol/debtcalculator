namespace com.debtcalculator.API.Models
{ 

    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long IdProfile { get; set; }
    }

    public static class UserCtrlModelExtensions
    {

        public static User ToVM(this Domain.Entities.User user)
        {
            return new User
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                IdProfile = user.IdProfile
            };
        }
    }
}