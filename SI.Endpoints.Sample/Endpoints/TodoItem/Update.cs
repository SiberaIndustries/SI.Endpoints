using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace SI.Endpoints.Sample.Endpoints.TodoItem
{
    public class Update : AsyncEndpoint<UpdateTodoItemCommand, UpdateTodoItemResponse>
    {
        private readonly IValidator<UpdateTodoItemCommand> updateTodoItemCommandValidator;

        public Update(IValidator<UpdateTodoItemCommand> updateTodoItemCommandValidator)
        {
            this.updateTodoItemCommandValidator = updateTodoItemCommandValidator;
        }
        
        [HttpPut]
        [HttpPatch]
        public override async Task<ActionResult<UpdateTodoItemResponse>> HandleAsync(UpdateTodoItemCommand request)
        {
            var validationResult = await updateTodoItemCommandValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToDictionary());
            }

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
