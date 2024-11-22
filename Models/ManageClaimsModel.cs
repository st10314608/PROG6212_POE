using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

public class ManageClaimsModel : PageModel
{
    private readonly AppDbContext _context;

    public ManageClaimsModel(AppDbContext context)
    {
        _context = context;
    }

    public IList<Claim> Claims { get; set; }

    public async Task OnGetAsync()
    {
        Claims = await _context.Claims
            .Where(c => c.Status == "Approved" && !c.InvoiceGenerated)
            .Include(c => c.Lecturer)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostGenerateReportAsync()
    {
        var claims = await _context.Claims
            .Where(c => c.Status == "Approved" && !c.InvoiceGenerated)
            .Include(c => c.Lecturer)
            .ToListAsync();

        // Generate the report using Crystal Reports or SSRS
        string reportPath = ReportGenerator.GenerateClaimsReport(claims);

        // Update database
        foreach (var claim in claims)
        {
            claim.InvoiceGenerated = true;
        }

        var report = new Report
        {
            ReportType = "Claims Report",
            GeneratedBy = User.Identity.Name,
            FilePath = reportPath
        };

        _context.Reports.Add(report);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Report generated successfully.";
        return RedirectToPage();
    }
}
