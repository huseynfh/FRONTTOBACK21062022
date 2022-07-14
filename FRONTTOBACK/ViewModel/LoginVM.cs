using System.ComponentModel.DataAnnotations;

namespace FRONTTOBACK.ViewModel
{
    public class LoginVM
    {
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string PassWord { get; set; }
    }
}
