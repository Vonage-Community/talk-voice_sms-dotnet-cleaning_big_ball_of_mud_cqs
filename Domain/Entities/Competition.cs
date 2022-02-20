using Domain.ValueObjects;

namespace Domain.Entities;

public class Competition
{
    public Guid Id { get; init; }

    public string Name { get; private set; }

    public bool IsLive { get; private set; }

    public bool IsClosed { get; private set; }

    public string TelephoneNumber { get; private set; }

    public Entry Winner { get; private set; }

    private List<Entry> _entries;

    public IReadOnlyCollection<Entry> Entries => _entries.AsReadOnly();

    private Competition() { }

    public Competition(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Competition Create(Guid id, string name)
    {
        return new Competition(id, name);
    }

    public void AddEntry(Entry entry)
    {
        if (!IsLive || IsClosed)
        {
            return;
        }

        _entries.Add(entry);
    }

    public void Start()
    {
        IsLive = true;
    }

    public void ChooseWinner()
    {
        if (Winner != null)
            throw new Exception("Cannot choose winner, winner has already been chosen.");
        
        IsClosed = true;
        IsLive = false;
        
        if (_entries.Any())
        {
            var random = new Random();
            int index = random.Next(_entries.Count);

            Winner = _entries[index];
        }
    }
}