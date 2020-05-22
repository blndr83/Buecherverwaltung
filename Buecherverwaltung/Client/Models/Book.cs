namespace Buecherverwaltung.Client.Models
{
    public partial class Book
    {
        public string ArticleNumber { get; set; }
        public string Title { get; set; }
        public bool IsLoaned { get; set; }
    }
}
