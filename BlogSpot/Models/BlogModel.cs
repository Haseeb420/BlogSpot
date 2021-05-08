using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace BlogSpot.Models
{
    public class BlogModel
    {
        public int Blog_ID { get; set; }

        [Required(ErrorMessage ="Title is Required!!!!!!!")]
        [MaxLength(100,ErrorMessage ="Title should not more then 100 characters....")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        public string AuthorName { get; set; }

        public DateTime PublicationDate { get; set; }

        [Required(ErrorMessage ="Content can't be Empty!!!!!!")]
        [MinLength(10, ErrorMessage = "Content should not less then 10 characters....")]
        [MaxLength(1000, ErrorMessage = "Content should not more then 1000 characters....")]
        public string Content { get; set; }
    }
}
