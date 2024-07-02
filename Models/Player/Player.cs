namespace Model.Player;

public class Player : IPlayer
{
    private int? _id;
    private int _score;

    public long? Id
    {
        get => _id;
        set => _id = value is null ? null : (int)value.Value;
    }
    
    public int? Score
    {
        get => _score;
        set => _score = value is null or < 0 ? 0 : value.Value;
    }
    
    public string? Name { get; set; }

    public int Arrows { get; private set; } = 5;
    public Location Location { get; set; } = new()
    {
        X = 0,
        Y = 0
    };
    
    public void Shoot() => Arrows--;
}