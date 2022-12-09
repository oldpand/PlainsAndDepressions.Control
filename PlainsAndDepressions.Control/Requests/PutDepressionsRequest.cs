using PlainsAndDepressions.Control.Contracts.Models;

namespace PlainsAndDepressions.Control.Requests;

public sealed class PutDepressionsRequest
{
    public Guid PackId { get; set; }
    
    public Depression[] Pack { get; set; } = null!;
}