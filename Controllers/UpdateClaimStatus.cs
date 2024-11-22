using Microsoft.AspNetCore.Mvc;

public async Task<IActionResult> UpdateClaimStatus(int id, string status, string rejectionReason = null)
{
    var claim = await _context.Claims.FindAsync(id);
    if (claim == null) return NotFound();

    claim.Status = status;
    if (status == "Rejected") claim.RejectionReason = rejectionReason;

    await _context.SaveChangesAsync();
    return Ok(new { Message = $"Claim {status} successfully!" });
}
