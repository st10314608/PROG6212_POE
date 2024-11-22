using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "HR, Manager")]
public class ReportsController : Controller
{
    private readonly ReportService _reportService;

    public ReportsController(ReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpPost]
    public async Task<IActionResult> GenerateInvoice()
    {
        string filePath = await _reportService.GenerateInvoiceAsync();
        TempData["SuccessMessage"] = $"Invoice generated: {filePath}";
        return RedirectToAction("ViewReports");
    }

    public async Task<IActionResult> ViewReports()
    {
        var reports = await _context.Reports.ToListAsync();
        return View(reports);
    }
}
