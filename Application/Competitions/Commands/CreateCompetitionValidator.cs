using FluentValidation;

namespace Application.Competitions.Commands
{
    public class CreateCompetitionValidator : AbstractValidator<CreateCompetition>
    {
        public CreateCompetitionValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required.");
        }
    }
}
