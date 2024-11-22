using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class CoordinatorController : Controller
{
    private readonly ClaimDbContext _context;

    public CoordinatorController(ClaimDbContext context)
    {
        _context = context;
    }

    // View all claims
    [HttpGet]
    public async Task<IActionResult> ReviewClaims()
    {
        var claims = await _context.Claims.ToListAsync();
        return View(claims);
    }

    // Approve claim
    [HttpPost]
    public async Task<IActionResult> ApproveClaim(int id)
    {
        var claim = await _context.Claims.FindAsync(id);

        if (claim != null && claim.IsValid == true)
        {
            claim.Status = "Approved";
            claim.ReviewerId = GetCurrentUserId();
            claim.ReviewerRole = GetCurrentUserRole();
            claim.ApprovalDate = DateTime.Now;

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Claim approved successfully!";
        }

        return RedirectToAction("ReviewClaims");
    }

    // Reject claim
    [HttpPost]
    public async Task<IActionResult> RejectClaim(int id, string rejectionReason)
    {
        var claim = await _context.Claims.FindAsync(id);

        if (claim != null)
        {
            claim.Status = "Rejected";
            claim.ReviewerId = GetCurrentUserId();
            claim.ReviewerRole = GetCurrentUserRole();
            claim.RejectionReason = rejectionReason;

            await _context.SaveChangesAsync();
            TempData["ErrorMessage"] = "Claim rejected.";
        }

        return RedirectToAction("ReviewClaims");
    }

    // Automated validation
    private bool ValidateClaim(Claim claim)
    {
        // Example validation rules
        if (claim.HoursWorked <= 0 || claim.HoursWorked > 160) return false;
        if (claim.HourlyRate <= 0 || claim.HourlyRate > 1000) return false;

        return true;
    }

    private int GetCurrentUserId()
    {
        // Simulated method to get the current user's ID
        return 123; // Replace with actual user context logic
    }

    private string GetCurrentUserRole()
    {
        // Simulated method to get the current user's role
        return "Coordinator"; // Replace with actual role-checking logic
    }
}

[Authorize(Roles = "Coordinator, Manager")]
public class CoordinatorController : Controller
{
    // Actions for coordinators and managers
}
