namespace BookService.Application.DTOs
{
    public class RegisterUserDto
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
    }
}
