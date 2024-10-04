namespace SocialNetworkApi.Models;

public class ValidationResponse
{
    public string PropertyName { get; set; }

    public string ErrorMessage { get; set; }

    public object AttemptedValue { get; set; }
}
