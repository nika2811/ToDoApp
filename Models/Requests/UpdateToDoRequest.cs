namespace ToDoApp.Models.Requests;

public class UpdateToDoRequest
{
    public string CurrentTitle { get; set; }
    public string NewTitle { get; set; }
    public string NewDescription { get; set; }
    public DateTime? NewDeadLine { get; set; }
}