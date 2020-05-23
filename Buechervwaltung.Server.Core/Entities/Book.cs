namespace Buecherverwaltung.Server.Core.Entities
{
    public partial class Book : Entity 
    {
        public string ArticleNumber { get; set; }
        public string Title { get; set; }
        public bool IsLoaned { get; set; }
    }
}
