using Microsoft.AspNetCore.Mvc;

[HttpPost]
public async Task<IActionResult> SubmitClaim([FromBody] Claim claim)
{
    var validator = new ClaimValidator();
    var result = validator.Validate(claim);

    if (!result.IsValid)
    {
        return BadRequest(result.Errors);
    }

    claim.TotalPayment = claim.HoursWorked * claim.HourlyRate;
    claim.Status = "Pending";
    claim.SubmissionDate = DateTime.Now;

    _context.Claims.Add(claim);
    await _context.SaveChangesAsync();

    return Ok(new { Message = "Claim submitted successfully!" });
}
 