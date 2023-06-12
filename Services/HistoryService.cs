using Middleware.Dto;
using Middleware.Interfaces;
using Middleware.Models.Entities;

namespace Middleware.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IRepository<LeapYearCheck> _repository;
        private readonly int _pageSize;

        public HistoryService(IRepository<LeapYearCheck> repository, IConfiguration configuration)
        {
            _repository = repository;
            _pageSize = int.TryParse(configuration["HistoryPageSize"], out var temp) ? temp : 20;
        }

        public bool DeleteEntry(int entryId, int userId)
        {
            var entry = _repository.Get(x => x.LeapYearCheckId == entryId)
                .FirstOrDefault();

            if (entry == null || entry.UserId == null || entry.UserId != userId)
            {
                return false;
            }

            _repository.Remove(entry);
            return _repository.SaveChanges() > 0;
        }

        public HistoryDto GetHistory(int pageIndex)
        {
            var checks = _repository.GetAll(x => x.User!)
                .OrderByDescending(x => x.CheckedAt)
                .Skip((pageIndex - 1) * _pageSize)
                .Take(_pageSize);

            return new HistoryDto()
            {
                Entries = checks.Select(x => new HistoryEntryDto()
                {
                    EntryId = x.LeapYearCheckId,
                    UserId = x.UserId,
                    UserName = x.User == null ? null : x.User.UserName,
                    Name = x.Name,
                    Year = x.Year,
                    IsLeap = x.IsLeap,
                    CheckedAt = x.CheckedAt
                })
            };
        }
    }
}
