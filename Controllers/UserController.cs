using Microsoft.AspNetCore.Mvc;
using prof_edna_teles_shop_api.DTOs;
using prof_edna_teles_shop_api.Services.Interfaces;

namespace prof_edna_teles_shop_api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        var users = await _userService.GetAllUsersAsync();

        return (users.Count > 0)
            ? Ok(users)
            : NoContent();
    }

    [HttpGet("{id}", Name = "GetUserByIdAsync")]
    public async Task<IActionResult> GetUserByIdAsync(long id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        return (user != null)
            ? Ok(user)
            : NotFound();
    }

    [HttpPost("{email}/products")]
    public async Task<IActionResult> GetProductsPurchasedByUser(string email)
    {
        var products = await _userService.GetProductsPurchased(email);

        return (products.Count > 0)
            ? Ok(products)
            : NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserPostDTO newUser)
    {
        if (ModelState.IsValid)
        {
            var createdUser = await _userService.CreateUserAsync(newUser);

            return (createdUser != null)
                ? CreatedAtRoute("GetUserByIdAsync", new { id = createdUser.Id }, createdUser)
                : BadRequest("Falha ao criar o usuário.");
        }

        return BadRequest(ModelState);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUserAsync([FromBody] UserPutDTO updatedUser)
    {
        if (ModelState.IsValid)
        {
            var isUpdated = await _userService.UpdateUserAsync(updatedUser);

            return (isUpdated)
                ? Ok()
                : BadRequest("Falha ao atualizar o usuário.");
        }

        return BadRequest(ModelState);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserByIdAsync(long id)
    {
        var isDeleted = await _userService.DeleteUserByIdAsync(id);

        return (isDeleted)
            ? Ok()
            : BadRequest("Falha ao deletar usuário.");
    }
}
