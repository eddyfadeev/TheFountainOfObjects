namespace Model.DataObjects;

public record PlayerDTO
{
    public long Id { get; init; }
    public string Name { get; init; }
    public long? Score { get; init; }
    public PlayerDTO(long id, string name, long? score)
    {
        id = Id;
        name = Name;
        score = Score;
    }

    public PlayerDTO()
    {
        
    }
    
    public (long, string, long?) Deconstruct()
    {
        return (Id, Name, Score);
    }
}