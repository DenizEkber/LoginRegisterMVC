namespace LoginAndRegister.Models
{
    public class UserProfileViewModel
    {
        public int Id { get; set; }
        public int Id_User {  get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] PhotoData { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
