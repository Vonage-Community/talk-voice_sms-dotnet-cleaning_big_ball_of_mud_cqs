using Application.Competitions.Commands;
using Application.Infrastructure.Validation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebOld.Pages
{
    public class AddEntryModel : PageModel
    {
        public void OnGet()
        {
            string competitionId = Request.Query["competitionId"];
            ViewData["CompetitionId"] = competitionId;
        }

        public async Task<IActionResult> OnPostAsync([FromServices] IMediator mediator)
        {
            var name = Request.Form["name"];
            var telephoneNumber = Request.Form["telephone"];
            var competitionId = Guid.Parse(Request.Form["competitionId"]);

            var command = new AddEntryToCompetition(competitionId, name, telephoneNumber);
            try
            {
                await mediator.Send(command);

                return RedirectToPage("Index");
            }
            catch (ValidationException ex)
            {

                return Page();
            }
        }
    }
}
