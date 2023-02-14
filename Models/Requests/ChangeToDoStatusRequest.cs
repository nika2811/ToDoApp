using ToDoApp.Db.Entities;

namespace ToDoApp.Models.Requests;

public class ChangeToDoStatusRequest
{
    public string Title { get; set; }
    public TodoStatus Status { get; set; }
}