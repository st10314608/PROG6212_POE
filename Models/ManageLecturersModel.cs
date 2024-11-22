using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class ManageLecturersModel : PageModel
{
    private readonly AppDbContext _context;

    public ManageLecturersModel(AppDbContext context)
    {
        _context = context;
    }

    public IList<Lecturer> Lecturers { get; set; }

    public async Task OnGetAsync()
    {
        Lecturers = await _context.Lecturers.ToListAsync();
    }

    public async Task<IActionResult> OnPostEditAsync(int id, string name, string phoneNumber)
    {
        var lecturer = await _context.Lecturers.FindAsync(id);
        if (lecturer != null)
        {
            lecturer.Name = name;
            lecturer.PhoneNumber = phoneNumber;
            await _context.SaveChangesAsync();
        }
        return RedirectToPage();
    }
}
