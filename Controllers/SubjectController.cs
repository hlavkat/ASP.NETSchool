using ASP.NETSchool.Models;
using ASP.NETSchool.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETSchool.Controllers {
    [Authorize(Roles = "Teacher, Admin, Student, Parent")]

    public class SubjectController : Controller {
		private SubjectService service;
		public SubjectController(SubjectService service) {
			this.service = service;
		}
		public async Task<IActionResult> IndexAsync() {
			var allSubject= await service.GetAllAsync();
			return View(allSubject);
		}
		[Authorize(Roles ="Admin,Teacher")]
		public IActionResult Create() {
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateAsync(Subject subject) {
			await service.CreateStudentAsync(subject);
			return RedirectToAction("Index");
		}
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Edit(int id) {
			var subjectToEdit = await service.GetById(id);
			if (subjectToEdit == null) {
				return View("NotFound");
			}
			return View(subjectToEdit);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Subject subject) {
			await service.UpdateAsync(subject);
			return RedirectToAction("Index");
		}
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Delete(int id) {
			var subjectToDelete = await service.GetById(id);
			if (subjectToDelete == null) {
				return View("NotFound");
			}
			await service.DeleteAsync(subjectToDelete);
			return RedirectToAction("Index");
		}
	}
}
