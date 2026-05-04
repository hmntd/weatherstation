namespace WeatherStation.BusinessLogic.DTOs.AuthDTOs;

using System.ComponentModel.DataAnnotations;

public class UserRegisterDto
{
    [Required(ErrorMessage = "Ім'я є обов'язковим")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Email є обов'язковим")]
    [EmailAddress(ErrorMessage = "Некоректний формат Email")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Пароль є обов'язковим")]
    [MinLength(6, ErrorMessage = "Пароль має містити мінімум 6 символів")]
    public string Password { get; set; } = null!;
}
