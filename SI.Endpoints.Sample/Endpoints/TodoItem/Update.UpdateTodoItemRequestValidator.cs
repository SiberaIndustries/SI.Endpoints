using FluentValidation;

namespace SI.Endpoints.Sample.Endpoints.TodoItem
{
    public class UpdateTodoItemCommandValidator : AbstractValidator<UpdateTodoItemCommand>
    {
        public UpdateTodoItemCommandValidator()
        {
            RuleFor(x => x.TodoItem).NotNull();
        }
    }
}
