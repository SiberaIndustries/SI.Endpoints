using Microsoft.AspNetCore.Mvc;

namespace SI.Endpoints.Sample.Endpoints.TodoItem
{
    public class Get : Endpoint<int, GetTodoItemResponse>
    {
        [HttpGet("{id}")]
        public override ActionResult<GetTodoItemResponse> Handle(int id)
        {
            if (id != 42)
            {
                return NotFound();
            }

            return new GetTodoItemResponse
            {
                Id = id,
                Title = "Go Shopping",
                Description = "Just go.."
            };
        }
    }
}
