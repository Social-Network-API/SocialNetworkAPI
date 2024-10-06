namespace SocialNetworkApi.Models;

public class ValidationResponse
{
    public string PropertyName { get; set; } = default!; 

    public string ErrorMessage { get; set; } = default!; 

    public object AttemptedValue { get; set; } = default!; 
}
