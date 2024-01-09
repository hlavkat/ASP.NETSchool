using ASP.NETSchool.Models;
using System.ComponentModel.DataAnnotations;

namespace ASP.NETSchool.ViewModels {
	public class GradeViewModel {
		public int Id { get; set; }
		[Display(Name ="Subject")]
		public int SubjectId { get; set; }
		
		public int Mark { get; set; }
		public string Topic { get; set; }
		public DateTime Date { get; set; }
		[Display(Name ="Student")]
		public int StudentId { get; set; }
	}
}
