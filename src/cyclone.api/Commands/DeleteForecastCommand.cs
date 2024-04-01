using cyclone.core.DTO;

using MediatR;

namespace cyclone.api.Commands;

public class DeleteForecastCommand: IRequest<bool>
{
    public int ForecastId { get; set; }

    public DeleteForecastCommand(int id)
    {
        ForecastId = id;
    }
}