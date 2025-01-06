namespace Interfaces
{
    public enum BookmarkType
    {
        birth, marriage, death, misc
    }

    [Flags]
    public enum BookType
    {
        None = 0, Mischbände=1, Taufbücher=2, Hochzeitsbücher=4, Sterbebücher=8, Verschiedenes=16
    }

    public enum BirthState
    {
        unknown, legitmate, illegitmate
    };

}
