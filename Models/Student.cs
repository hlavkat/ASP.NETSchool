using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP.NETSchool.Models {
    public class Student {
        [DisplayName ("Id")]
        public int Id { get; set; }
		[DisplayName("First name")]
		public string? FirstName { get; set; }
		[DisplayName("Last name")]
		public string? LastName { get; set;}
		[DisplayName("Date of birth")]
		public DateTime DateOfBirth { get; set; }

    }
}
