namespace SocialNetwork.Models;

public class ServiceResult
{
    public bool Success { get; set; }

    public ValidationResponse[] Errors { get; set; } = [];
}
