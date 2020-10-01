using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SI.Endpoints.Sample.Endpoints.TodoItem
{
    public class List : EndpointWithResponse<IEnumerable<TodoItemResponse>>
    {
        [HttpGet]
        public override ActionResult<IEnumerable<TodoItemResponse>> Handle()
        {
            return new[]
            {
                new TodoItemResponse
                {
                    Id = 1,
                    Title = "Title A",
                    Description = "Description A"
                },
                new TodoItemResponse
                {
                    Id = 1,
                    Title = "Title B",
                    Description = "Description B"
                }
            };
        }
    }
}
