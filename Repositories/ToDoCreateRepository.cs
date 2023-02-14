using ToDoApp.DB;
using ToDoApp.Db.Entities;
using ToDoApp.Models.Requests;

namespace ToDoApp.Repositories;

public interface IToDoCreateRepository
{
    Task InsertAsync(int userId, string title, string description, DateTime deadline);
    Task SaveChangesAsync();
    List<TodoEntity> Search(SearchRequest request);
    List<TodoEntity> Read();
    Task UpdateToDoAsync(UpdateToDoRequest request);
    Task ChangeStatus(ChangeToDoStatusRequest request);
}

public class ToDoCreateRepository : IToDoCreateRepository
{
    private readonly AppDbContext _db;

    public ToDoCreateRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task InsertAsync(
        int userId,
        string title,
        string description,
        DateTime deadline)
    {
        var entity = new TodoEntity();
        entity.UserId = userId;
        entity.Title = title;
        entity.Description = description;
        entity.Deadline = deadline;
        entity.Satatus = TodoStatus.New;
        entity.CreatedAt = DateTime.UtcNow;

        await _db.Todos.AddAsync(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }

    public List<TodoEntity> Read()
    {
        var data = _db.Todos.OrderBy(x => x.Deadline).ToList();
        return data;
    }

    public List<TodoEntity> Search(SearchRequest request)
    {
        var entities = _db.Todos
            .Where(t => t.UserId == 1)
            .Where(t => t.Title.Contains(request.Filter))
            .Skip(request.PageIndex * request.PageSize)
            .Take(request.PageSize)
            .OrderBy(t => t.Deadline)
            .ToList();

        return entities;
    }

    public Task UpdateToDoAsync(UpdateToDoRequest request)
    {
        var toDo = _db.Todos.First(t => t.Title == request.CurrentTitle);
        if (!string.IsNullOrEmpty(request.NewTitle)) toDo.Title = request.NewTitle;

        if (!string.IsNullOrEmpty(request.NewDescription)) toDo.Description = request.NewDescription;

        if (request.NewDeadLine.HasValue) toDo.Deadline = (DateTime)request.NewDeadLine;

        return Task.CompletedTask;
    }

    public Task ChangeStatus(ChangeToDoStatusRequest request)
    {
        var toDo = _db.Todos.First(t => t.Title == request.Title);

        toDo.Satatus = request.Status;
        return Task.CompletedTask;
    }
}