using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AG_MT2.Models;
using AP_MT2.Models;

namespace AG_MT2.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SRMSContext _context;

        public StudentsController(SRMSContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            return _context.Students != null ?
                        View(await _context.Students.ToListAsync()) :
                        Problem("Entity set 'SRMSContext.Students'  is null.");
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(m => m.Courses)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            List<SelectableCourse> selCourses = new List<SelectableCourse>();
            foreach (Course course in _context.Courses)
            {
                bool isSelected = student.Courses.Contains(course);
                selCourses.Add(new SelectableCourse(course.Id, course.CourseNumber, course.Title, course.Credits, isSelected));
            }

            Enrollment enrollmentVM = new Enrollment(student, selCourses);

            return View(enrollmentVM);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,DateOfBirth")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        public async Task<IActionResult> EditEnrollment(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            List<SelectableCourse> selCourses = new List<SelectableCourse>();
            foreach (Course course in _context.Courses)
            {
                bool isSelected = student.Courses.Contains(course);
                selCourses.Add(new SelectableCourse(course.Id, course.CourseNumber, course.Title, course.Credits, isSelected));
            }

            Enrollment enrollmentVM = new Enrollment(student, selCourses);


            return View(enrollmentVM);
        }

        [HttpPost]
        public IActionResult EditEnrollment(Enrollment enrollmentVM)
        {
            if (enrollmentVM?.Student?.Id == null)
            {
                return BadRequest("Invalid student data.");
            }

            var student = _context.Students.Include(s => s.Courses).FirstOrDefault(s => s.Id == enrollmentVM.Student.Id);

            if (student == null)
            {
                return NotFound("Student not found.");
            }

            student.Courses.Clear();

            foreach (var selCourse in enrollmentVM.SelectableCourses.Where(c => c.IsSelected))
            {
                var course = _context.Courses.Include(c => c.Students).FirstOrDefault(c => c.Id == selCourse.CourseId);
                if (course != null)
                {
                    // Delete all students from a course
                    course.Students.Clear();

                    // Go through all the students, and if that student has this course, add this student to that course
                    var studentsWithCourse = _context.Students.Include(s => s.Courses).Where(s => s.Courses.Any(c => c.Id == course.Id));
                    foreach (var studentWithCourse in studentsWithCourse)
                    {
                        course.Students.Add(studentWithCourse);
                    }
                }
                student.Courses.Add(course);
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = student.Id });
        }


        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,DateOfBirth")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'SRMSContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
