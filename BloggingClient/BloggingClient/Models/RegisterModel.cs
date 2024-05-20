using System.ComponentModel.DataAnnotations;

namespace BloggingClient.Models;

public class RegisterModel
{
    [Required]
    public string CNP { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain at least 8 characters: uppercase letter, lowercase letter, number and a special character [@$!%*?&]")]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    [Required]
    public byte[] Document { get; set; }

    [Required]
    public byte[] ProfileImage { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email.")]
    public string Email { get; set; }

    [Required]
    public string UserName { get; set; }
}