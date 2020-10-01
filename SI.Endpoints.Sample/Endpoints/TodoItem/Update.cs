using Microsoft.AspNetCore.Mvc;

namespace SI.Endpoints.Sample.Endpoints.TodoItem
{
    public class Update : Endpoint<UpdateTodoItemCommand, UpdateTodoItemResponse>
    {
        [HttpPut]
        [HttpPatch]
        public override ActionResult<UpdateTodoItemResponse> Handle(UpdateTodoItemCommand request)
        {
            if (request.Id != 42)
            {
                return NotFound();
            }

            return new UpdateTodoItemResponse
            {
                Id = request.Id,
                Title = request.TodoItem!.Title,
                Description = request.TodoItem!.Description
            };
        }
    }
}
