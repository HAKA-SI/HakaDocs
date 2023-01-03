namespace API.Dtos
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public string PhotoUrl { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
       public int? HaKaDocClientId { get; set; }
    }
}