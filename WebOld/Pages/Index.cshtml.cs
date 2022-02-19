using Application.Competitions.Models;
using Application.Competitions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebOld.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ISender _sender;

        public IEnumerable<CompetitionModel> Competitions { get; private set; }


        public IndexModel(ILogger<IndexModel> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        public async Task OnGetAsync()
        {
            var query = new FindCompetitions(1, 10);
            Competitions = await _sender.Send(query);
        }
    }
}