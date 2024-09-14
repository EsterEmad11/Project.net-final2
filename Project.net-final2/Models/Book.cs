using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Project.net_final2.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Title is Required.")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 40 Charcter.")]
        [DisplayName("Title of Book")]
        public string title { get; set; }

        [Range(100, 5000, ErrorMessage = "Price must be between 100 and 5000.")]
        [DisplayName("Book Price")]
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Book is Required.")]
        [DisplayName("Book Quantity")]
        public int Quantity { get; set; }


        [Required(ErrorMessage = "Description is Required.")]
        [DisplayName("Book Description")]
        public string Description { get; set; }


        public string? ImagePath { get; set; }

        [NotMapped]
        public IFormFile? clientFile { get; set; }

        
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
