using System.ComponentModel.DataAnnotations;

namespace BloggingClient.Models;

public class RegisterModel
{
    [Required(ErrorMessage = "You must set a password!")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain at least 8 characters: uppercase letter, lowercase letter, number and a special character [@$!%*?&]")]
    public string Password { get; set; }

    [Required(ErrorMessage = "You must retype the password!")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "The confirm password must be the same with the password!")]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "You must set an email!")]
    [EmailAddress(ErrorMessage = "Invalid email format!")]
    public string Email { get; set; }

    [Required(ErrorMessage = "You must set an username!")]
    public string UserName { get; set; }

    public byte[] ProfileImage { get; set; }

    [Required]
    [Range(typeof(bool), "true", "true", ErrorMessage = "You must be agree with our terms & conditions")]
    public bool AcceptTerms { get; set; }
}