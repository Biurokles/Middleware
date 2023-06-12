namespace Middleware.Dto
{
    public class HistoryEntryDto
    {
        public int EntryId { get; set; }
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public int Year { get; set; }
        public bool IsLeap { get; set; }
        public DateTime CheckedAt { get; set; }
    }
}
