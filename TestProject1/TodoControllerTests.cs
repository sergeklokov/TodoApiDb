using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using TodoApiDb.Controllers;
using TodoApiDb.Data;
using TodoApiDb.Models;

namespace TestProject1
{
    public class TodoControllerTests
    {
        private readonly Mock<ITodoItemsService> _mockTodoItemsService;

        public TodoControllerTests()
        {
            _mockTodoItemsService = new Mock<ITodoItemsService>();
        }

        //not working
        [Fact]
        public async Task GetABunch_ReturnsFilteredTodoItems()
        {
            // Arrange
            var mockData = new List<TodoItem>
            {
                new TodoItem { Id = 1, Name = "dog" },
                new TodoItem { Id = 2, Name = "cat" },
                new TodoItem { Id = 3, Name = "bird" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<TodoItem>>();
            mockSet.As<IQueryable<TodoItem>>().Setup(m => m.Provider).Returns(mockData.Provider);
            mockSet.As<IQueryable<TodoItem>>().Setup(m => m.Expression).Returns(mockData.Expression);
            mockSet.As<IQueryable<TodoItem>>().Setup(m => m.ElementType).Returns(mockData.ElementType);
            mockSet.As<IQueryable<TodoItem>>().Setup(m => m.GetEnumerator()).Returns(mockData.GetEnumerator());
            mockSet.As<IAsyncEnumerable<TodoItem>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<TodoItem>(mockData.GetEnumerator()));

            var mockContext = new Mock<TodoContext>();
            //mockContext.Setup(c => c.TodoItems).Returns(mockSet.Object);
            mockContext.Setup(c => c.Set<TodoItem>()).Returns(mockSet.Object);

            var controller = new TodoItemsController(mockContext.Object);

            // Act
            var result = await controller.GetABunch();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedItems = Assert.IsType<List<TodoItem>>(actionResult.Value);
            Assert.Equal(2, returnedItems.Count);
            Assert.Contains(returnedItems, i => i.Name == "dog");
            Assert.Contains(returnedItems, i => i.Name == "cat");
        }

        // Working
        [Fact]
        public async Task GetABunch120_InMemoryDB_ReturnsFilteredTodoItems()
        {
            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            using var context = new TodoContext(options);
            context.TodoItems.AddRange(
                new TodoItem { Id = 1, Name = "dog" },
                new TodoItem { Id = 2, Name = "cat" },
                new TodoItem { Id = 3, Name = "bird" });
            await context.SaveChangesAsync();

            var controller = new TodoItemsController(context);
            var result = await controller.GetABunch120();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var items = Assert.IsType<List<TodoItem>>(okResult.Value);
            Assert.Equal(2, items.Count);
            Assert.Contains(items, i => i.Name == "dog");
            Assert.Contains(items, i => i.Name == "cat");
        }
    }
}

// Helper class to mock async enumerator
internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
{
    private readonly IEnumerator<T> _inner;
    public TestAsyncEnumerator(IEnumerator<T> inner) => _inner = inner;
    public ValueTask<bool> MoveNextAsync() => new ValueTask<bool>(_inner.MoveNext());
    public T Current => _inner.Current;
    public ValueTask DisposeAsync() { _inner.Dispose(); return default; }
}