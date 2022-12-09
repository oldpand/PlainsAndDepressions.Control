namespace PlainsAndDepressions.Control.Contracts.Data;

public class Depression
{
    public Depression(Guid packID, int size)
    {
        PackID = packID;
        Size = size;
    }
    public int Id { get; set; }
    public Guid PackID { get; set; }
    public int Size { get; set; }
}
