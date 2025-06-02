namespace Infrastructure.FileStorage;

public class MinioOptions
{
    public string AccessKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public string PublicEndpoint { get; set; } = string.Empty;
    public string InternalEndpoint { get; set; } = string.Empty;
}

