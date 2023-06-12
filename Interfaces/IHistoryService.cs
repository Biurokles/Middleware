using Middleware.Dto;

namespace Middleware.Interfaces
{
    public interface IHistoryService
    {
        HistoryDto GetHistory(int pageIndex);

        bool DeleteEntry(int entryId, int userId);
    }
}
