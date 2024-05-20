using System.ComponentModel.DataAnnotations;

namespace BloggingClient.Models;

public class AccountDetailsModel
{
    public string CNP { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    [Required]
    public string ProfileImage { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email.")]
    public string Email { get; set; }

    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain at least 8 characters: uppercase letter, lowercase letter, number and a special character [@$!%*?&]")]
    public string OldPassword { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain at least 8 characters: uppercase letter, lowercase letter, number and a special character [@$!%*?&]")]
    public string NewPassword { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(NewPassword))]
    public string ConfirmPassword { get; set; }

    public AccountDetailsModel()
    { }

    public AccountDetailsModel(AccountDetailsModel user)
    {
        CNP = user.CNP;
        PhoneNumber = user.PhoneNumber;
        ProfileImage = user.ProfileImage;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Email = user.Email;
        UserName = user.UserName;
        NewPassword = user.NewPassword;
        ConfirmPassword = user.ConfirmPassword;
        OldPassword = user.OldPassword;
    }
}