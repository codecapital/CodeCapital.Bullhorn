namespace CodeCapital.Bullhorn.Dtos
{
    public class FileAttachmentDto
    {
        public int Id { get; set; }
        public string ContentSubType { get; set; } = "";
        public string ContentType { get; set; } = "";
        public long DateAdded { get; set; }
        public string FileExtension { get; set; } = "";
        public int FileSize { get; set; }
        public string FileType { get; set; } = "";
        public string Name { get; set; } = "";
        public string Type { get; set; } = "";
        public CandidateDto Candidate { get; set; } = new CandidateDto();
    }
}
