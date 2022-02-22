using MediatR;

namespace Application.Competitions.Commands
{
    public class CreateCompetition : IRequest
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Telephone { get; }

        public CreateCompetition(Guid id, string name, string telephone)
        {
            Id = id;
            Name = name;
            Telephone = telephone;
        }
    }
}
