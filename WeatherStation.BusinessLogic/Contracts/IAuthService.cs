namespace WeatherStation.BusinessLogic.Contracts;

using WeatherStation.BusinessLogic.DTOs.AuthDTOs;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(UserRegisterDto registerDto);
    Task<AuthResponseDto> LoginAsync(UserLoginDto loginDto);
}
