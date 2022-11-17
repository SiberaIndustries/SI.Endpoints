using Microsoft.AspNetCore.Mvc;

namespace SI.Endpoints.Sample.Endpoints.TodoItem
{
    public class List : AsyncEnumerableEndpointWithResponse<ListTodoItemResponse>
    {
        [HttpGet]
        public override ActionResult<IAsyncEnumerable<ListTodoItemResponse>> HandleAsync()
        {
            var todos = new[]
            {
                new ListTodoItemResponse
                {
                    Id = 1,
                    Title = "Title A",
                    Description = "Description A"
                },
                new ListTodoItemResponse
                {
                    Id = 2,
                    Title = "Title B",
                    Description = "Description B"
                }
            };

            async IAsyncEnumerable<ListTodoItemResponse> todosAsync()
            {
                foreach (var todo in todos)
                {
                    yield return todo;
                    await Task.Delay(1000);
                }
            }

            return Ok(todosAsync());
        }
    }
}
