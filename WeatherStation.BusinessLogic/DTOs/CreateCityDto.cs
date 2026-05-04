namespace WeatherStation.BusinessLogic.DTOs;

using System.ComponentModel.DataAnnotations;
using WeatherStation.BusinessLogic.Validation;

public class CreateCityDto
{
    [Display(Name = "Назва міста")]
    [Required(ErrorMessage = "Поле '{0}' є обов'язковим.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "{0} має містити від {2} до {1} символів.")]
    [CapitalLetter(ErrorMessage = "Помилка: Назва міста має починатися з великої літери!")]
    public string Name { get; set; } = null!;

    [Display(Name = "Широта (Latitude)")]
    [Required(ErrorMessage = "Вкажіть широту.")]
    [Range(-90.0, 90.0, ErrorMessage = "Широта має бути в межах від -90 до 90.")]
    public double Latitude { get; set; }

    [Display(Name = "Довгота (Longitude)")]
    [Required(ErrorMessage = "Вкажіть довготу.")]
    [Range(-180.0, 180.0, ErrorMessage = "Довгота має бути в межах від -180 до 180.")]
    public double Longitude { get; set; }
}
