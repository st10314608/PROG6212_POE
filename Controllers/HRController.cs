using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "HR")]
public class HRController : Controller
{
    private readonly AppDbContext _context;

    public HRController(AppDbContext context)
    {
        _context = context;
    }

    // View all lecturers
    [HttpGet]
    public async Task<IActionResult> ViewLecturers()
    {
        var lecturers = await _context.Lecturers.ToListAsync();
        return View(lecturers);
    }

    // Add or Edit Lecturer
    [HttpPost]
    public async Task<IActionResult> ManageLecturer(Lecturer model)
    {
        if (ModelState.IsValid)
        {
            if (model.LecturerId == 0)
            {
                _context.Lecturers.Add(model);
            }
            else
            {
                _context.Lecturers.Update(model);
            }
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Lecturer data saved successfully.";
        }
        return RedirectToAction("ViewLecturers");
    }

    // View claims and approve in bulk
    [HttpGet]
    public async Task<IActionResult> ProcessClaims()
    {
        var claims = await _context.Claims.Where(c => c.Status == "Approved" && c.HRProcessed == false).ToListAsync();
        return View(claims);
    }

    [HttpPost]
    public async Task<IActionResult> BulkProcessClaims()
    {
        var claims = await _context.Claims.Where(c => c.Status == "Approved" && c.HRProcessed == false).ToListAsync();
        foreach (var claim in claims)
        {
            claim.HRProcessed = true;
            claim.PaymentSummary = $"Payment processed for ${claim.TotalPayment} on {DateTime.Now}";
        }
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "All claims processed successfully.";
        return RedirectToAction("ProcessClaims");
    }
}
