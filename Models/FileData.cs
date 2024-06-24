using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZenkoAPI.Models
{
    public class FileData
    {
        [Key]
        [Required]
        public required Guid FileId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public decimal FileSize { get; set; }
        public DateTime UploadTime { get; set; }
        [ForeignKey("UserId")]
        public required Guid UserId { get; set; }
    }
}
