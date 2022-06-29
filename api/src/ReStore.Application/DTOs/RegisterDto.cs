namespace ReStore.Application.DTOs;

public class RegisterDto : LoginDto
{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
}
