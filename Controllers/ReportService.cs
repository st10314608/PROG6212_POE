using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Reflection.Metadata;

public class ReportService
{
    private readonly AppDbContext _context;

    public ReportService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<string> GenerateInvoiceAsync()
    {
        var approvedClaims = await _context.Claims
            .Where(c => c.Status == "Approved")
            .Include(c => c.Lecturer)
            .ToListAsync();

        string filePath = $"Reports/Invoice_{DateTime.Now:yyyyMMddHHmmss}.pdf";

        // Generate PDF
        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        {
            Document doc = new Document();
            PdfWriter.GetInstance(doc, fs);
            doc.Open();

            // Add content
            doc.Add(new Paragraph("Monthly Claims Invoice"));
            doc.Add(new Paragraph($"Date: {DateTime.Now:yyyy-MM-dd}"));
            doc.Add(new Paragraph("--------------------------------------------------"));

            foreach (var claim in approvedClaims)
            {
                doc.Add(new Paragraph($"Lecturer: {claim.Lecturer.Name}"));
                doc.Add(new Paragraph($"Hours Worked: {claim.HoursWorked}"));
                doc.Add(new Paragraph($"Hourly Rate: ${claim.HourlyRate}"));
                doc.Add(new Paragraph($"Total Payment: ${claim.TotalPayment}"));
                doc.Add(new Paragraph("--------------------------------------------------"));
            }

            doc.Close();
        }

        // Save report log to database
        var report = new Report
        {
            ReportType = "Invoice",
            GeneratedBy = "System",
            FilePath = filePath
        };
        _context.Reports.Add(report);
        await _context.SaveChangesAsync();

        return filePath;
    }
}
