using Application.Competitions.Commands;
using Application.Infrastructure.Validation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebOld.Pages;

public class CreateCompetitionModel : PageModel
{
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync([FromServices]IMediator mediator)
    {
        var name = Request.Form["name"];

        var command = new CreateCompetition(Guid.NewGuid(), name);
        try
        {
            await mediator.Send(command);

            return RedirectToPage("Index");
        }
        catch (ValidationException ex)
        {
            ViewData["Errors"] = ex.Message;
            return Page();
        }
    }
}