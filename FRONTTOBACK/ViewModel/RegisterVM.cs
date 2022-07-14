using System.ComponentModel.DataAnnotations;

namespace FRONTTOBACK.ViewModel
{
    public class RegisterVM
    {
        [Required,StringLength(100)]
        public string FullName { get; set; }

        [Required, StringLength(100)]
        public string UserName { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string PassWord{ get; set; }

        [Required, DataType(DataType.Password),Compare("PassWord")]
        public string RepeatPassWord { get; set; }

    }
}
