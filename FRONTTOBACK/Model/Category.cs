using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FRONTTOBACK.Model
{
    public class Category
    {
        public int Id { get; set; }


        [Required (ErrorMessage = "bos olmaz"), MinLength(5, ErrorMessage = "en az 5 simvol")]
        public string Name { get; set; }

        [Required(ErrorMessage = "bos olmaz"), MinLength(10, ErrorMessage = "en az 10 simvol")]
        public string Desc { get; set; }

  
        public List<Product> Products { get; set; }
    }
}
