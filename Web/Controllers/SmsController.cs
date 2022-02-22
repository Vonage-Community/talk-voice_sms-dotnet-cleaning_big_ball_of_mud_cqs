using Application.Competitions.Commands;
using Application.Competitions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vonage.Messaging;
using Vonage.Utility;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SmsController : ControllerBase
{
    private readonly ISender _sender;

    public SmsController(ISender sender)
    {
        _sender = sender ?? throw new ArgumentNullException(nameof(sender));
    }
    
    [HttpGet("inbound")]
    public async Task<ActionResult> Inbound()
    { 
        // parse inbound sms
        var inboundSms = WebhookParser.ParseQuery<InboundSms>(Request.Query);
        
        // get competition
        var query = new GetCompetitionByTelephone(inboundSms.To);
        var competition = await _sender.Send(query);
        if (competition == null)
            return NotFound();
        
        // add entry to competition
        var command = new AddEntryToCompetition(competition.Id, inboundSms.Text, inboundSms.Msisdn);
        await _sender.Send(command);

        return Ok();
    }
}