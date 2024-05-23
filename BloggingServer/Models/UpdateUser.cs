namespace Models;

public class UpdateUser
{
    public string Id { get; set; }

    public string Email { get; set; }

    public string ProfileImage { get; set; }

    public string OldPassword { get; set; }

    public string NewPassword { get; set; }
}