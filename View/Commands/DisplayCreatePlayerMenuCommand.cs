﻿using View.Interfaces;
using View.Views.CreatePlayerMenu;

namespace View.Commands;

public class DisplayCreatePlayerMenuCommand : ICommand
{
    private readonly ILayoutManager _layoutManager;
    
    public DisplayCreatePlayerMenuCommand(ILayoutManager layoutManager)
    {
        _layoutManager = layoutManager;
    }
    
    public Enum Execute()
    {
        var createPlayerView = new CreatePlayerView(_layoutManager);
        return createPlayerView.DisplaySelectable();
    }
}