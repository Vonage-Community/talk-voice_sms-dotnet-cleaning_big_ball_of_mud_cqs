namespace Application.Competitions.Models;

public class CompetitionModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public bool IsLive { get; set; }
    public bool IsClosed { get; set; }

    public bool HasWinner { get; set; }

    public string Winner { get; set; }

    public IReadOnlyCollection<EntryModel> Entries { get; set; }
}