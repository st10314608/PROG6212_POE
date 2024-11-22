using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ClaimsApiController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClaimsApiController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllClaims()
    {
        var claims = await _context.Claims.ToListAsync();
        return Ok(claims);
    }

    [HttpPost]
    public async Task<IActionResult> SubmitClaim([FromBody] Claim claim)
    {
        if (ModelState.IsValid)
        {
            claim.TotalPayment = claim.HoursWorked * claim.HourlyRate;
            claim.Status = "Pending";
            claim.SubmissionDate = DateTime.Now;

            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Claim submitted successfully!" });
        }
        return BadRequest(ModelState);
    }
}
