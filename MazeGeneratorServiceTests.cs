
// CreateTable initializes a table with the correct field size
[Fact]
public void create_table_initializes_correct_field_size()
{
    var mockGameSettingsRepository = new Mock<IGameSettingsRepository>();
    var mockPlayerRepository = new Mock<IPlayerRepository>();
    var mockMazeObjectFactory = new Mock<IMazeObjectFactory>();
    var mockMazeService = new Mock<IMazeService<IRoom>>();
    var mockRoomPopulator = new Mock<IRoomPopulator>();

    mockMazeService.Setup(m => m.MazeSize).Returns(5);

    var service = new MazeGeneratorService(
        mockGameSettingsRepository.Object,
        mockPlayerRepository.Object,
        mockMazeObjectFactory.Object,
        mockMazeService.Object,
        mockRoomPopulator.Object
    );

    var table = service.CreateTable();

    Assert.Equal(5, table.Columns.Count);
}

// CreateTable populates the table with generated rooms
[Fact]
public void create_table_populates_with_generated_rooms()
{
    var mockGameSettingsRepository = new Mock<IGameSettingsRepository>();
    var mockPlayerRepository = new Mock<IPlayerRepository>();
    var mockMazeObjectFactory = new Mock<IMazeObjectFactory>();
    var mockMazeService = new Mock<IMazeService<IRoom>>();
    var mockRoomPopulator = new Mock<IRoomPopulator>();

    mockMazeService.Setup(m => m.MazeSize).Returns(5);

    var service = new MazeGeneratorService(
        mockGameSettingsRepository.Object,
        mockPlayerRepository.Object,
        mockMazeObjectFactory.Object,
        mockMazeService.Object,
        mockRoomPopulator.Object
    );

    var table = service.CreateTable();

    mockRoomPopulator.Verify(rp => rp.GenerateRooms(mockMazeService.Object), Times.Once);
}

// UpdateTable initializes a table with the correct field size
[Fact]
public void update_table_initializes_correct_field_size()
{
    var mockGameSettingsRepository = new Mock<IGameSettingsRepository>();
    var mockPlayerRepository = new Mock<IPlayerRepository>();
    var mockMazeObjectFactory = new Mock<IMazeObjectFactory>();
    var mockMazeService = new Mock<IMazeService<IRoom>>();
    var mockRoomPopulator = new Mock<IRoomPopulator>();

    mockMazeService.Setup(m => m.MazeSize).Returns(5);
    mockMazeService.Setup(m => m.MazeRooms).Returns(new IRoom[5, 5]);

    var service = new MazeGeneratorService(
        mockGameSettingsRepository.Object,
        mockPlayerRepository.Object,
        mockMazeObjectFactory.Object,
        mockMazeService.Object,
        mockRoomPopulator.Object
    );

    var table = service.UpdateTable();

    Assert.Equal(5, table.Columns.Count);
}

// UpdateTable populates the table with existing rooms
[Fact]
public void update_table_populates_with_existing_rooms()
{
    var mockGameSettingsRepository = new Mock<IGameSettingsRepository>();
    var mockPlayerRepository = new Mock<IPlayerRepository>();
    var mockMazeObjectFactory = new Mock<IMazeObjectFactory>();
    var mockMazeService = new Mock<IMazeService<IRoom>>();
    var mockRoomPopulator = new Mock<IRoomPopulator>();

    var rooms = new IRoom[5, 5];
    mockMazeService.Setup(m => m.MazeSize).Returns(5);
    mockMazeService.Setup(m => m.MazeRooms).Returns(rooms);

    var service = new MazeGeneratorService(
        mockGameSettingsRepository.Object,
        mockPlayerRepository.Object,
        mockMazeObjectFactory.Object,
        mockMazeService.Object,
        mockRoomPopulator.Object
    );

    var table = service.UpdateTable();

    Assert.Equal(5, table.Rows.Count);
}

// InitializeTable creates a table with no border and no headers
[Fact]
public void initialize_table_creates_no_border_no_headers()
{
    var mockGameSettingsRepository = new Mock<IGameSettingsRepository>();
    var mockPlayerRepository = new Mock<IPlayerRepository>();
    var mockMazeObjectFactory = new Mock<IMazeObjectFactory>();
    var mockMazeService = new Mock<IMazeService<IRoom>>();
    var mockRoomPopulator = new Mock<IRoomPopulator>();

    var service = new MazeGeneratorService(
        mockGameSettingsRepository.Object,
        mockPlayerRepository.Object,
        mockMazeObjectFactory.Object,
        mockMazeService.Object,
        mockRoomPopulator.Object
    );

    var table = service.CreateTable();

    Assert.Equal(TableBorder.None, table.Border);
    Assert.False(table.ShowHeaders);
}

// CreateTable handles zero field size gracefully
[Fact]
public void create_table_handles_zero_field_size_gracefully()
{
    var mockGameSettingsRepository = new Mock<IGameSettingsRepository>();
    var mockPlayerRepository = new Mock<IPlayerRepository>();
    var mockMazeObjectFactory = new Mock<IMazeObjectFactory>();
    var mockMazeService = new Mock<IMazeService<IRoom>>();
    var mockRoomPopulator = new Mock<IRoomPopulator>();

    mockMazeService.Setup(m => m.MazeSize).Returns(0);

    var service = new MazeGeneratorService(
        mockGameSettingsRepository.Object,
        mockPlayerRepository.Object,
        mockMazeObjectFactory.Object,
        mockMazeService.Object,
        mockRoomPopulator.Object
    );

    var table = service.CreateTable();

    Assert.Empty(table.Columns);
}

// UpdateTable handles zero field size gracefully
[Fact]
public void update_table_handles_zero_field_size_gracefully()
{
    var mockGameSettingsRepository = new Mock<IGameSettingsRepository>();
    var mockPlayerRepository = new Mock<IPlayerRepository>();
    var mockMazeObjectFactory = new Mock<IMazeObjectFactory>();
    var mockMazeService = new Mock<IMazeService<IRoom>>();
    var mockRoomPopulator = new Mock<IRoomPopulator>();

    var rooms = new IRoom[0, 0];
    mockMazeService.Setup(m => m.MazeSize).Returns(0);
    mockMazeService.Setup(m => m.MazeRooms).Returns(rooms);

    var service = new MazeGeneratorService(
        mockGameSettingsRepository.Object,
        mockPlayerRepository.Object,
        mockMazeObjectFactory.Object,
        mockMazeService.Object,
        mockRoomPopulator.Object
    );

    var table = service.UpdateTable();

    Assert.Empty(table.Columns);
}

// AddColumns handles zero columns gracefully
[Fact]
public void add_columns_handles_zero_columns_gracefully()
{
    var table = new Table();
    
    var serviceType = typeof(MazeGeneratorService);
    
    // Using reflection to access the private method AddColumns
    var methodInfo = serviceType.GetMethod("AddColumns", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
    
    // Creating an instance of MazeGeneratorService with mocks
    var serviceInstance = (IMazeGeneratorService)Activator.CreateInstance(serviceType, 
      It.IsAny<IGameSettingsRepository>(), 
      It.IsAny<IPlayerRepository>(), 
      It.IsAny<IMazeObjectFactory>(), 
      It.IsAny<IMazeService<IRoom>>(), 
      It.IsAny<IRoomPopulator>());
    
    methodInfo.Invoke(serviceInstance, new object[] { table, 0 });
    
    Assert.Empty(table.Columns);
}

// AddRows handles zero rows gracefully
[Fact]
public void add_rows_handles_zero_rows_gracefully()
{
  var table = new Table();
  
  var serviceType = typeof(MazeGeneratorService);
  
  // Using reflection to access the private method AddRows
  var methodInfo = serviceType.GetMethod("AddRows", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
  
  // Creating an instance of MazeGeneratorService with mocks
  var serviceInstance = (IMazeGeneratorService)Activator.CreateInstance(serviceType, 
      It.IsAny<IGameSettingsRepository>(), 
      It.IsAny<IPlayerRepository>(), 
      It.IsAny<IMazeObjectFactory>(), 
      It.IsAny<IMazeService<IRoom>>(), 
      It.IsAny<IRoomPopulator>());
  
  methodInfo.Invoke(serviceInstance, new object[] { table, 0 });
  
  Assert.Empty(table.Rows);
}

// PopulateTable handles empty maze service gracefully
[Fact]
public void populate_table_handles_empty_maze_service_gracefully()
{
  var table = new Table();
  
  var serviceType = typeof(MazeGeneratorService);
  
  // Using reflection to access the private method PopulateTable
  var methodInfo = serviceType.GetMethod("PopulateTable", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
  
  // Creating an instance of MazeGeneratorService with mocks
  var serviceInstance = (IMazeGeneratorService)Activator.CreateInstance(serviceType, 
      It.IsAny<IGameSettingsRepository>(), 
      It.IsAny<IPlayerRepository>(), 
      It.IsAny<IMazeObjectFactory>(), 
      It.IsAny<IMazeService<IRoom>>(), 
      It.IsAny<IRoomPopulator>());
  
  methodInfo.Invoke(serviceInstance, new object[] { table, 0 });
  
  Assert.Empty(table.Rows);
  Assert.Empty(table.Columns);
}

