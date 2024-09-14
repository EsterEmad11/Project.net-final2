using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Project.net_final2.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required.")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 25 Charcter.")]
        [DisplayName("Category Name")]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }
}
