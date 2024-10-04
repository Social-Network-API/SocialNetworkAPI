namespace SocialNetworkApi.Models;

public class ServiceResult
{
    public bool Success { get; set; }

    public ValidationResponse[] Errors { get; set; } = Array.Empty<ValidationResponse>();
}
