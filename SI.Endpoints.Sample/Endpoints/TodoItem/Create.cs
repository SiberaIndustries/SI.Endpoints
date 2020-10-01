using Microsoft.AspNetCore.Mvc;
using System;

namespace SI.Endpoints.Sample.Endpoints.TodoItem
{
    public class Create : Endpoint<CreateTodoItemCommand, CreateTodoItemResponse>
    {
        [HttpPost]
        public override ActionResult<CreateTodoItemResponse> Handle(CreateTodoItemCommand request)
        {
            return new CreateTodoItemResponse
            {
                Id = new Random().Next(0, int.MaxValue),
                Title = request.Title,
                Description = request.Description
            };
        }
    }
}
