﻿namespace View.Views.GameView;

public class GameView : IDisplayable
{
    public string MenuName { get; }

    public ILayoutManager LayoutManager { get; }
    
    public GameView(ILayoutManager layoutManager)
    {
        MenuName = "Game";
        LayoutManager = layoutManager;
    }
}