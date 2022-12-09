using MediatR;
using PlainsAndDepressions.Control.Contracts.Models;
using PlainsAndDepressions.Control.Services.Results;

namespace PlainsAndDepressions.Control.Services.Commands;

public class PutPuckCommand : IRequest<ControlledPackResult>
{
    public PutPuckCommand(Guid packId, Depression[] pack)
    {
        PackId = packId;
        Pack = pack;
    }

    public Guid PackId { get; }

    public Depression[] Pack { get; } = null!;
}
