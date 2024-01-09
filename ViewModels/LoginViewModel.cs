using System.ComponentModel;

namespace ASP.NETSchool.ViewModels {
	public class LoginViewModel {
        [DisplayName("User name")]
        public string Username { get; set; }
		[DisplayName("Password")]
		public string Password { get; set; }
        public string? ReturnUrl { get; set; }
        public bool Remember {  get; set; }
    }
}
