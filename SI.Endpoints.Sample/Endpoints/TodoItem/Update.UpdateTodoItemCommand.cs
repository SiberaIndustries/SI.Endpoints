namespace SI.Endpoints.Sample.Endpoints.TodoItem
{
    public class UpdateTodoItemCommand
    {
        public int Id { get; set; }

        public UpdateTodoItemResponse? TodoItem { get; set; }
    }
}
