using Microsoft.AspNetCore.Mvc;

namespace SI.Endpoints.Sample.Endpoints.TodoItem
{
    public class Delete : EndpointWithRequest<int>
    {
        [HttpDelete("{id}")]
        public override ActionResult Handle(int id)
        {
            if (id == 42)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
