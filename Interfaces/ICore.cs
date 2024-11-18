namespace Interfaces
{
    public interface ICore
    {
        public IEnumerable<IParish> Parishes { get; }
        public List<String> Favorites { get; }

        public void saveNotes();
    }
}
