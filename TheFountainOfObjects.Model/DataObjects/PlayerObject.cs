namespace TheFountainOfObjects.Model.DataObjects;

internal record PlayerObject
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public int? Score { get; init; }

    public PlayerObject() {}
    
    public PlayerObject(int id, string name, int score)
    {
        Id = id;
        Name = name;
        Score = score;
    }
}
