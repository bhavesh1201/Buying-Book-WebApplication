using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class Category
    {


        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is Required")]
        [DataType(DataType.Text)]
        [DisplayName("Name")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Range(0,100,ErrorMessage ="Order should be in range")]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
    }
}
