namespace Shared.Models
{
    public class Project
    {
        public string Token { get; set; } = Guid.NewGuid().ToString();
        public string Code { get; set; } = string.Empty;
        public int Version { get; set; } = 0;
        public bool UpdateRequested { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
