using Microsoft.AspNetCore.Mvc;

[HttpPost]
public async Task<IActionResult> SubmitClaim(Claim model)
{
    // Validate inputs
    if (ModelState.IsValid)
    {
        model.TotalPayment = model.HoursWorked * model.HourlyRate;
        model.SubmissionDate = DateTime.Now;
        model.Status = "Pending";
        model.IsValid = ValidateClaim(model);

        _context.Claims.Add(model);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Claim submitted successfully!";
        return RedirectToAction("ClaimList");
    }

    return View(model);
}

