using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Middleware.Dto;
using Middleware.Interfaces;
using Middleware.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Middleware.Pages
{
    public class HistoryModel : PageModel
    {
        private readonly IHistoryService _historyService;
        private readonly UserManager<User> _userManager;

        [BindProperty]
        [Required]
        public int DeleteEntryId { get; set; }

        public HistoryDto Dto { get; private set; }
        public int? currentUserId { get; private set; }

        public HistoryModel(IHistoryService historyService, UserManager<User> userManager)
        {
            _historyService = historyService;
            _userManager = userManager;
        }

        public void OnGet(int? pageIndex)
        {
            currentUserId = int.TryParse(_userManager.GetUserId(User), out var temp) ? temp : null;

            Dto = _historyService.GetHistory(pageIndex ?? 1);
        }

        public IActionResult OnPost(int? pageIndex)
        {
            currentUserId = int.TryParse(_userManager.GetUserId(User), out var temp) ? temp : null;
            Dto = _historyService.GetHistory(pageIndex ?? 1);

            if (!ModelState.IsValid || currentUserId == null)
                return Page();

            _historyService.DeleteEntry(DeleteEntryId, (int)currentUserId);

            return Page();
        }
    }
}
