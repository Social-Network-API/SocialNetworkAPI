using Microsoft.AspNetCore.Mvc;
using SocialNetworkApi.Mappers.Request.Comment;
using SocialNetworkApi.Services;

namespace SocialNetworkApi.Controllers;

[ApiController]
[Route("api/v1/posts/{postId:guid}/comments")]
public class CommentsController : ControllerBase
{
    private readonly CommentsService _commentService;

    public CommentsController(CommentsService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid postId, [FromBody] CreateCommentRequest request)
    {
        var comment = request.ToDomain(postId);
        var result = await _commentService.CreateAsync(comment);

        if (!result.Success || result.Data == null)
        {
            return BadRequest("Failed to create comment.");
        }

        return CreatedAtAction(nameof(GetAllComments), new { postId, commentId = result.Data.CommentId }, result.Data);
    }


    [HttpGet]
    public async Task<IActionResult> GetAllComments([FromRoute] Guid postId)
    {
        var result = await _commentService.GetAllByPostIdAsync(postId);

        if (!result.Success)
            return NotFound($"No comments found for postId {postId}");

        return Ok(result.Data);
    }
    
    [HttpPut("{commentId:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid commentId, [FromBody] EditCommentRequest request)
    {
        var comment = request.ToDomain();
        var result = await _commentService.UpdateAsync(commentId , comment);
        return result.Success
            ? Ok(result.Data)
            : Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Failed to update post");
    } 
        
    [HttpDelete("{commentId:guid}")] 
    public async Task<IActionResult> Delete(Guid commentId)
    {
        await _commentService.DeleteAsync(commentId);
        return NoContent(); 
    }
}

