namespace ArchiveBrowser.ViewModels
{
    public class ItemVM : BaseViewModel
    {
        public int Indent { get; protected set; } = 0;
        virtual public bool IsSelected { get; set; } = false;
    }
}
