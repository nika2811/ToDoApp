namespace ToDoApp.Models.Requests;

public class SearchRequest
{
    public string Filter { get; set; }
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
}