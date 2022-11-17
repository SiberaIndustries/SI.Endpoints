namespace SI.Endpoints.Sample.Endpoints.TodoItem
{
    public class ListTodoItemResponse
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }
    }
}