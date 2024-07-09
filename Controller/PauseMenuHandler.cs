using Interfaces.View.LayoutManager;

namespace Controller;

public class PauseMenuHandler
{
    private readonly ILayoutManager _layoutManager;

    public PauseMenuHandler(ILayoutManager layoutManager)
    {
        _layoutManager = layoutManager;
    }
    
}