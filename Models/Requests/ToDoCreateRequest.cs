namespace ToDoApp.Models.Requests;

public class ToDoCreateRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DeadLine { get; set; }
}