using DataObjects.Player;
using Services.Extensions;

namespace Utilities.Tests.Extensions;

public class PlayerMapperExtensionsTests
{
    private const int DefaultScore = 0;
    
    [Fact]
    public void converting_fully_populated_playerdto_to_player()
    {
        var dto = new PlayerDTO { Id = 1, Name = "John", Score = 100 };
        
        var player = dto.ToDomain();
        
        Assert.Equal(dto.Id, player.Id);
        Assert.Equal(dto.Name, player.Name);
        Assert.Equal(dto.Score, player.Score);
    }
    
    [Fact]
    public void converting_fully_populated_player_to_playerdto()
    {
        var player = new Player { Id = 1, Name = "John", Score = 100 };
        
        var dto = player.ToDto();
        
        Assert.Equal(player.Id, dto.Id);
        Assert.Equal(player.Name, dto.Name);
        Assert.Equal(player.Score, dto.Score);
    }
    
    [Fact]
    public void converting_playerdto_with_null_score_to_player_with_default_score()
    {
        var dto = new PlayerDTO { Id = 1, Name = "John", Score = null };
        
        var player = dto.ToDomain();
        
        Assert.Equal(dto.Id, player.Id);
        Assert.Equal(dto.Name, player.Name);
        Assert.Equal(DefaultScore, player.Score);
    }
    
    [Fact]
    public void converting_player_with_zero_score_to_playerdto()
    {
        var player = new Player { Id = 1, Name = "John", Score = 0 };
        
        var dto = player.ToDto();
        
        Assert.Equal(player.Id, dto.Id);
        Assert.Equal(player.Name, dto.Name);
        Assert.Equal(player.Score, dto.Score);
    }
    
    [Fact]
    public void converting_playerdto_with_null_name_to_player()
    {
        var dto = new PlayerDTO { Id = 1, Name = null, Score = 100 };
        
        var player = dto.ToDomain();
        
        Assert.Equal(dto.Id, player.Id);
        Assert.Null(player.Name);
        Assert.Equal(dto.Score, player.Score);
    }
    
    [Fact]
    public void converting_player_with_null_name_to_playerdto()
    {
        var player = new Player { Id = 1, Name = null, Score = 100 };
        
        var dto = player.ToDto();
        
        Assert.Equal(player.Id, dto.Id);
        Assert.Null(dto.Name);
        Assert.Equal(player.Score, dto.Score);
    }
    
    [Fact]
    public void converting_playerdto_with_negative_score_to_player()
    {
        var dto = new PlayerDTO { Id = 1, Name = "John", Score = -10 };
        
        var player = dto.ToDomain();
        
        Assert.Equal(dto.Id, player.Id);
        Assert.Equal(dto.Name, player.Name);
        Assert.Equal(DefaultScore, player.Score);
    }
    
    [Fact]
    public void converting_player_with_negative_score_to_playerdto()
    {
        var player = new Player { Id = 1, Name = "John", Score = -10 };
        
        var dto = player.ToDto();
        
        Assert.Equal(player.Id, dto.Id);
        Assert.Equal(player.Name, dto.Name);
        Assert.Equal(player.Score, dto.Score);
    }

    // Converting a PlayerDTO with maximum integer Score to Player
    [Fact]
    public void converting_playerdto_with_maximum_integer_score_to_player()
    {
        var dto = new PlayerDTO { Id = 1, Name = "John", Score = int.MaxValue };
        
        var player = dto.ToDomain();
        
        Assert.Equal(dto.Id, player.Id);
        Assert.Equal(dto.Name, player.Name);
        Assert.Equal(dto.Score, player.Score);
    }

    // Converting a Player with maximum integer Score to PlayerDTO
    [Fact]
    public void converting_player_with_maximum_integer_score_to_playerdto()
    {
        var player = new Player { Id = 1, Name = "John", Score = int.MaxValue };
        
        var dto = player.ToDto();
        
        Assert.Equal(player.Id, dto.Id);
        Assert.Equal(player.Name, dto.Name);
        Assert.Equal(player.Score, dto.Score);
    }

    // Converting a PlayerDTO with empty string Name to Player
    [Fact]
    public void converting_playerdto_with_empty_string_name_to_player()
    {
        var dto = new PlayerDTO { Id = 1, Name = "", Score = 100 };
        
        var player = dto.ToDomain();
        
        Assert.Equal(dto.Id, player.Id);
        Assert.Equal(dto.Name, player.Name);
        Assert.Equal(dto.Score, player.Score);
    }

    // Converting a Player with empty string Name to PlayerDTO
    [Fact]
    public void converting_player_with_empty_string_name_to_playerdto()
    {
        var player = new Player { Id = 1, Name = "", Score = 100 };
        
        var dto = player.ToDto();
        
        Assert.Equal(player.Id, dto.Id);
        Assert.Equal(player.Name, dto.Name);
        Assert.Equal(player.Score, dto.Score);
    }
    
    [Fact]
    public void converting_playerdto_with_special_characters_to_player()
    {
        var dto = new PlayerDTO { Id = 1, Name = "J@ne", Score = 100 };
        
        var player = dto.ToDomain();
        
        Assert.Equal(dto.Id, player.Id);
        Assert.Equal(dto.Name, player.Name);
        Assert.Equal(dto.Score, player.Score);
    }
    
    [Fact]
    public void converting_player_with_special_characters_to_playerdto()
    {
        var player = new Player { Id = 1, Name = "M@ry", Score = 150 };
        
        var dto = player.ToDto();
        
        Assert.Equal(player.Id, dto.Id);
        Assert.Equal(player.Name, dto.Name);
        Assert.Equal(player.Score, dto.Score);
    }
    
    [Fact]
    public void ensuring_immutability_of_original_objects_after_conversion()
    {
        var dto = new PlayerDTO { Id = 1, Name = "Alice", Score = 200 };
        var originalDto = new PlayerDTO { Id = dto.Id, Name = dto.Name, Score = dto.Score };
        var player = dto.ToDomain();
        var originalPlayer = new Player { Id = player.Id, Name = player.Name, Score = player.Score };
    
        Assert.Equal(originalDto.Id, dto.Id);
        Assert.Equal(originalDto.Name, dto.Name);
        Assert.Equal(originalDto.Score, dto.Score);
    
        Assert.Equal(originalPlayer.Id, player.Id);
        Assert.Equal(originalPlayer.Name, player.Name);
        Assert.Equal(originalPlayer.Score, player.Score);
    }
}