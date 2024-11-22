using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class LecturerController : Controller
{
    private readonly AppDbContext _context;

    public LecturerController(AppDbContext context)
    {
        _context = context;
    }

    // View and Edit Lecturer Profile
    [HttpGet]
    public async Task<IActionResult> EditProfile()
    {
        var lecturer = await _context.Lecturers
            .FirstOrDefaultAsync(l => l.Email == User.Identity.Name);
        if (lecturer == null) return NotFound();

        return View(lecturer);
    }

    [HttpPost]
    public async Task<IActionResult> EditProfile(Lecturer lecturer)
    {
        if (ModelState.IsValid)
        {
            _context.Lecturers.Update(lecturer);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Profile updated successfully.";
        }
        return View(lecturer);
    }
}
