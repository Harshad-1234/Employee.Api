using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Employee.Api.Model
{
    public class EmployeeData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employeeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string firstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string lastName { get; set; }
        public string email { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(10)]
        public string contactNo { get; set; }
        public string city { get; set; }
        public string address { get; set; }




    }
}
