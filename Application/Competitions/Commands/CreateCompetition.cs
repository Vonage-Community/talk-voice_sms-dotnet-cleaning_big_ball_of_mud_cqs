using MediatR;

namespace Application.Competitions.Commands
{
    public class CreateCompetition : IRequest
    {
        public Guid Id { get; }
        public string Name { get; }

        public CreateCompetition(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
