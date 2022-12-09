using PlainsAndDepressions.Control.Contracts.Errors;

namespace PlainsAndDepressions.Control.Services.Results;

public class ControlledPackResult
{
    public ControlledPackResult()
    {
    }

    public ControlledPackResult(Error error)
    {
        AddError(error);
    }

    public IList<Error> Errors { get; } = new List<Error>();

    public bool IsSuccess { get; private set; } = true;

    public void AddError(Error error)
    {
        IsSuccess = false;
        Errors.Add(error);
    }
}