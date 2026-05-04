namespace WeatherStation.BusinessLogic.DTOs;

public class CreateCityDto
{
    public string Name { get; set; } = null!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}