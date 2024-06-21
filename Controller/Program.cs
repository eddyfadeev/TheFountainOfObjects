using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Rendering;
using View.Enums;
using View.Interfaces;

namespace Controller;
class Program
{
    private static void Main(string[] args)
    {
        //! Example: Dependency Injection
        
        // Create a service collection
        var serviceCollection = new ServiceCollection();
        // Configure services
        ConfigureServices.Configure(serviceCollection);
        // Build the service provider
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Example usage
        var commandFactory = serviceProvider.GetRequiredService<IMenuCommandFactory>();

        var mainMenuCommand = commandFactory.Create(MenuType.MainMenu);
        //mainMenuCommand.Execute();

        // Define the size of the grid
        int rows = 5;
        int cols = 5;

        // Create a 2D array to store the canvases
        Canvas[,] canvases = new Canvas[rows, cols];

        // Initialize the canvases
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                // Determine the cell color
                var color = (row + col) % 2 == 0 ? Color.White : Color.Olive; // Change the condition to your logic

                // Create a 3x3 canvas with the cell color
                var canvas = new Canvas(3, 3);
                for (int x = 0; x < 3; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        canvas.SetPixel(x, y, color);
                    }
                }

                canvases[row, col] = canvas;
            }
        }

        // Create the grid
        var grid = new Grid();

        // Add columns to the grid
        for (int col = 0; col < cols; col++)
        {
            grid.AddColumn(new GridColumn().NoWrap().PadLeft(0).PadRight(0));
        }

        // Add rows to the grid
        for (int row = 0; row < rows; row++)
        {
            var rowItems = new IRenderable[cols];
            for (int col = 0; col < cols; col++)
            {
                rowItems[col] = canvases[row, col];
            }
            grid.AddRow(rowItems);
        }

        canvases[0,3].SetPixel(1, 1, Color.Blue); // Change the cell content
        // Render the grid
        AnsiConsole.Render(grid);
    }
}