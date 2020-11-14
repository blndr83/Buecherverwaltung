using System.ComponentModel.DataAnnotations;

namespace Buecherverwaltung.Shared
{
    public partial class BookDto
    {
        public string ArticleNumber { get; set; }
        [Required]
        public string Title { get; set; }
        public bool IsLoaned { get; set; }
    }
}
