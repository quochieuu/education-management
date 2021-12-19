using System.ComponentModel.DataAnnotations;

namespace Edu.Data.Entities
{
    public class Teacher
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Teacher name is required")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter Mobile No")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "please enter correct address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Address is required")]
        [StringLength(300)]
        public string Address { get; set; }
    }
}
