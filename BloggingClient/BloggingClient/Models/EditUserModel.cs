using System.ComponentModel.DataAnnotations;

namespace BloggingClient.Models;

public class EditUserModel
{
    [Required(ErrorMessage = "You must set the old password!")]
    [DataType(DataType.Password)]
    public string OldPassword { get; set; }

    [Required(ErrorMessage = "You must set a new password!")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain at least 8 characters: uppercase letter, lowercase letter, number and a special character [@$!%*?&]")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "You must retype the new password!")]
    [DataType(DataType.Password)]
    [Compare(nameof(NewPassword), ErrorMessage = "The confirm new password must be the same with the new password!")]
    public string ConfirmNewPassword { get; set; }

    public byte[] ProfileImage { get; set; }
}