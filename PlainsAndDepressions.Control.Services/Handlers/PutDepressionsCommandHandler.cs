using MediatR;
using PlainsAndDepressions.Control.Contracts.Data;
using PlainsAndDepressions.Control.Contracts.Enum;
using PlainsAndDepressions.Control.Contracts.Errors;
using PlainsAndDepressions.Control.Services.Commands;
using PlainsAndDepressions.Control.Services.Context;
using PlainsAndDepressions.Control.Services.Results;

namespace PlainsAndDepressions.Control.Services.Handlers;

public class PutDepressionsCommandHandler : IRequestHandler<PutPuckCommand, ControlledPackResult>
{
    public async Task<ControlledPackResult> Handle(PutPuckCommand request, CancellationToken cancellationToken)
    {
        using (var context = new PlainsAndDepressionsContext())
        {
            if (context.Packs.Any(p => p.Id == request.PackId))
            {
                return new ControlledPackResult(
                    new Error(
                        (int)ErrorCodes.AlreadyExists, 
                        "Already exists"));
            }

            context.Packs.Add(new Pack(request.PackId));
            foreach (var dep in request.Pack)
            {
                context.Depressions.Add(new Depression(request.PackId, dep.Size));
            }

            await context.SaveChangesAsync();
        }

        return new ControlledPackResult();
    }
}
