using Microsoft.AspNetCore.Mvc;

public async Task<IActionResult> ManageLecturer(Lecturer model)
{
    var validator = new LecturerValidator();
    var result = validator.Validate(model);

    if (!result.IsValid)
    {
        return View(model);
    }

    // Save logic
    
}
