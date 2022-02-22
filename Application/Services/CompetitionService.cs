using System;
using Application.Infrastructure.Communications;
using Application.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
	public class CompetitionService
	{
		private readonly ILogger<CompetitionService> _logger;
		private readonly ICompetitionDbContext _db;
		private readonly ISmsSender _smsSender;

		public CompetitionService(ILogger<CompetitionService> logger,
			ICompetitionDbContext db,
			ISmsSender smsSender)
		{
			_logger = logger;
			_db = db;
			_smsSender = smsSender;
		}

		public async Task ChooseWinner(Guid competitionId, CancellationToken cancellationToken)
        {
			_logger.LogInformation("Choose Winner started.");

            try
            {
				Competition competition = await _db.Competitions
					.FirstOrDefaultAsync(x => x.Id == competitionId);

				if (!competition.IsLive)
				{
					throw new ValidationException("Competition either does not exist or is not live");
				}

				competition.ChooseWinner();
				await _db.SaveChangesAsync(cancellationToken);

				await _smsSender.SendSms(competition.Winner.TelephoneNumber,
					$"Congratulations you have won the {competition.Name} competition.");
			}
			catch(Exception ex)
            {
				_logger.LogError(ex, "Choose winner error.");
            }

            _logger.LogInformation("Choose Winner finished.");
		}
	}
}

