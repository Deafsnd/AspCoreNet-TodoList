// using AspNetCoreTodo.Models;  // Import your model namespace
// using AspNetCoreTodo.Services; // Import your service namespace
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;

// namespace AspNetCoreTodo.Api
// {
//     [ApiController]
//     [Route("api/todoitems")]
//     public class TodoItemController : ControllerBase
//     {
//         private readonly ITodoItemService _todoItemService;
//         private readonly UserManager<IdentityUser> _userManager;

//         // Constructor to inject services
//         public TodoItemController(ITodoItemService todoItemService, UserManager<IdentityUser> userManager)
//         {
//             _todoItemService = todoItemService;
//             _userManager = userManager;
//         }

//         // GET api/todoitems/{id}
//         [HttpGet("{id}")]
//         public async Task<IActionResult> GetById(Guid id)
//         {
//             var user = await _userManager.GetUserAsync(User);
//             if (user == null)
//                 return Unauthorized();  // Return 401 Unauthorized if user is not authenticated

//             var item = await _todoItemService.GetItemByIdAsync(user, id);
//             if (item == null)
//                 return NotFound();  // Return 404 if item is not found

//             return Ok(item);  // Return 200 OK with item
//         }

//         // GET api/todoitems
//         [HttpGet]
//         public async Task<IActionResult> GetAll()
//         {
//             var user = await _userManager.GetUserAsync(User);
//             if (user == null)
//                 return Unauthorized();  // Return 401 Unauthorized if user is not authenticated

//             var items = await _todoItemService.GetIncompleteItemsAsync(user);
//             return Ok(items);  // Return 200 OK with list of incomplete items
//         }

//         // POST api/todoitems/completed/{id}
//         [HttpPost("completed/{id}")]
//         public async Task<IActionResult> MarkCompleted(Guid id)
//         {
//             var user = await _userManager.GetUserAsync(User);
//             if (user == null)
//                 return Unauthorized();  // Return 401 Unauthorized if user is not authenticated

//             var result = await _todoItemService.MarkDoneAsync(id, user);  // Assuming MarkDoneAsync is the correct method
//             if (result)
//                 return Ok();  // Return 200 OK if the operation succeeded

//             return NotFound();  // Return 404 if item not found
//         }

//         // POST api/todoitems
//         [HttpPost]
//         public async Task<IActionResult> AddItem([FromBody] TodoItem value)
//         {
//             if (!ModelState.IsValid)
//             {
//                 return BadRequest(ModelState);  // Return 400 if model state is invalid
//             }

//             var user = await _userManager.GetUserAsync(User);
//             if (user == null)
//                 return Challenge("User not found");  // Return 401 if the user is not found

//             var createdItem = await _todoItemService.AddItemAsync(user, value);
//             return CreatedAtAction(nameof(GetById), new { id = createdItem.Id }, createdItem);  // Return 201 Created with item
//         }

//         // PUT api/todoitems/{id}
//         [HttpPut("{id}")]
//         public async Task<IActionResult> UpdateItem(Guid id, [FromBody] TodoItem value)
//         {
//             if (!ModelState.IsValid)
//             {
//                 return BadRequest(ModelState);  // Return 400 if model state is invalid
//             }

//             var user = await _userManager.GetUserAsync(User);
//             if (user == null)
//                 return Unauthorized("User not found");  // Return 401 if user is not found

//             var updatedItem = await _todoItemService.UpdateItemAsync(user, id, value);
//             if (updatedItem == null)
//                 return NotFound();  // Return 404 if item is not found to update

//             return NoContent();  // Return 204 No Content after successful update
//         }

//         // DELETE api/todoitems/{id}
//         [HttpDelete("{id}")]
//         public async Task<IActionResult> DeleteItem(Guid id)
//         {
//             var user = await _userManager.GetUserAsync(User);
//             if (user == null)
//                 return Unauthorized("User not found");  // Return 401 if user is not found

//             var deleted = await _todoItemService.DeleteItemAsync(user, id);
//             if (!deleted)
//                 return NotFound();  // Return 404 if item not found to delete

//             return NoContent();  // Return 204 No Content after successful deletion
//         }

//         // HEAD api/todoitems/{id}
//         [HttpHead("{id}")]
//         public async Task<IActionResult> ProductMetadata(Guid id)
//         {
//             var user = await _userManager.GetUserAsync(User);
//             if (user == null)
//                 return Challenge(); 

//             var item = await _todoItemService.GetItemByIdAsync(user, id);
//             if (item == null)
//             {
//                 return NotFound();  // Return 404 if item does not exist
//             }

//             Response.Headers.Add("X-LastUpdated", item.LastUpdated.ToString());
//             return Ok();  // Return 200 OK with metadata headers
//         }
//     }
// }

