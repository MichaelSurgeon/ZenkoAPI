namespace ZenkoAPI.Models
{
    public class FileData
    {
        public required int FileId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public decimal FileSize { get; set; }
        public DateTime UploadTime { get; set; }
        public required int UserId { get; set; }
    }
}
