using System.ComponentModel.DataAnnotations;

namespace AdminEmployeeManagement.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Employee Name is required")]
        [RegularExpression("^[a-z A-Z]*$", ErrorMessage = "Only Alphabets and Space allowed!")]
        [StringLength(100, MinimumLength = 1)]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email Address is required!")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [MaxLength(100)]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Please enter a valid email address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required!")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(10)]
        [Phone]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string? Mobile { get; set; }


        [Required(ErrorMessage = "Department Name is required!")]
        public int Department_Id { get; set; }
        public bool Status { get; set; }
    }
}
