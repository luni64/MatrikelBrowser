namespace Interfaces
{
    public enum BookmarkType
    {
        birth, marriage, death, misc
    }

    [Flags]
    public enum BookType
    {
        None = 0, Mischbände=1, Taufen=2, Trauungen=4, Sterbefälle=8, Verschiedenes=16
    }

    public enum birthState
    {
        unknown, legitmate, illegitmate
    };

}
