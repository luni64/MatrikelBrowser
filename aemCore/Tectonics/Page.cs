namespace AEM
{
    public class Page 
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Folio { get; set; }
        public required string ImageLink { get; set; }
        public required Book Book { get; set; }

        public override string ToString() => ImageLink;

        public string loadImage() { return string.Empty; }
    }


}
