using ASP.NETSchool.Models;
using ASP.NETSchool.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETSchool.Services {
	public class GradeService {
		private ApplicationDbContext dbContext;

		public GradeService(ApplicationDbContext dbContext) {
			this.dbContext = dbContext;
		}
		public async Task<GradesDropdownsViewModel> GetDropdownValuesAsync() {
			return new GradesDropdownsViewModel() {
				Students = await dbContext.Students.OrderBy(x=>x.LastName).ToListAsync(),
				Subjects = await dbContext.Subjects.ToListAsync(),
			};
		}

		internal async Task CreateAsync(GradeViewModel gradeModel) {
			var gradeToInsert = new Grade() {
				Student = dbContext.Students.FirstOrDefault(student => student.Id == gradeModel.StudentId),
				Subject=dbContext.Subjects.FirstOrDefault(subject=>subject.Id==gradeModel.SubjectId),
				Date=DateTime.Today,
				Topic=gradeModel.Topic,
				Mark=gradeModel.Mark
			};
			if (gradeToInsert.Student !=null && gradeToInsert.Subject !=null) {
				await dbContext.Grades.AddAsync(gradeToInsert);
				await dbContext.SaveChangesAsync(); 
			} 
		}
		internal async Task<IEnumerable<Grade>> GetAllGrades() {
			return await dbContext.Grades.Include(grade=>grade.Student).Include(grade=>grade.Subject).ToListAsync();
		}

		internal GradeViewModel GetById(int id) {
			var gradeToEdit=dbContext.Grades.FirstOrDefault(grade=>grade.Id==id);
			//GradeViewModel gradeViewModel = new GradeViewModel();
			if (gradeToEdit!=null) {
				return new GradeViewModel() {
					SubjectId = gradeToEdit.Subject.Id,
					StudentId = gradeToEdit.Student.Id,
					Id = gradeToEdit.Id,
					Mark = gradeToEdit.Mark,
					Date = gradeToEdit.Date,
					Topic = gradeToEdit.Topic
				};
			}
			return null;
		}

		internal async Task UpdateAsync(GradeViewModel updateGrade) {
			var gradeToUpdate=dbContext.Grades.FirstOrDefault(grade=>grade.Id==updateGrade.Id);
			if (gradeToUpdate!=null) {
				gradeToUpdate.Subject = dbContext.Subjects.FirstOrDefault(subject => subject.Id == updateGrade.SubjectId);
				gradeToUpdate.Student = dbContext.Students.FirstOrDefault(student => student.Id == updateGrade.StudentId);
				gradeToUpdate.Topic=updateGrade.Topic;
				gradeToUpdate.Mark=updateGrade.Mark;
				//gradeToUpdate.Date=updateGrade.Date;
			}
			dbContext.Update(gradeToUpdate);
			await dbContext.SaveChangesAsync();
		}
		internal async Task DeleteAsync(int id) {
			var gradesToDelete=dbContext.Grades.FirstOrDefault(grade=>grade.Id == id);
			if (gradesToDelete!=null) {
				dbContext.Grades.Remove(gradesToDelete);
				await dbContext.SaveChangesAsync();
			}
		}
	}
}

