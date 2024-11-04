namespace WPF_Challenge.DTOs;

public class UserDto
{
    public long Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Phone { get; set; }

    public int UserStatus { get; set; }
}
