namespace Models;

public class UpdateUser
{
    public string Username { get; set; }

    public string ProfileImage { get; set; }

    public string OldPassword { get; set; }

    public string NewPassword { get; set; }
}