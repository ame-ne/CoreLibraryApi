namespace CoreLibraryApi.Models
{
    public class Blob : BaseEntity
    {
        public byte[] Content { get; set; }
        public int Length { get; set; }
        public Attachment Attachment { get; set; }
    }
}
