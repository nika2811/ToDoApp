﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Db.Entities;
using ToDoApp.Models.Requests;
using ToDoApp.Repositories;

namespace ToDoApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoController : ControllerBase
{
    private readonly IToDoCreateRepository _todoRepository;
    private readonly UserManager<UserEntity> _userManager;

    public ToDoController(
        UserManager<UserEntity> userManager,
        IToDoCreateRepository todoRepository)
    {
        _userManager = userManager;
        _todoRepository = todoRepository;
    }

    [Authorize("ApiUser", AuthenticationSchemes = "Bearer")]
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] ToDoCreateRequest request)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null) return NotFound("User not found");

        var userId = user.Id;
        await _todoRepository.InsertAsync(userId, request.Title, request.Description, request.DeadLine);
        await _todoRepository.SaveChangesAsync();
        return Ok();
    }

    // list/search - Get user todo list / Search
    // update - update todo title, description and deadline
    // change-status - Change todo status
}