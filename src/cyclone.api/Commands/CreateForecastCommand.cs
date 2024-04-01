using cyclone.core.DTO;

using MediatR;

namespace cyclone.api.Commands;

// TODO
// maybe i need a CreateForecastResponse type? see video
public class CreateForecastCommand : IRequest
{
    public CitizenForecast Forecast { get; set; } = null!;

    public CreateForecastCommand(CitizenForecast forecast)
    {
        Forecast = forecast;
    }
}