public class ClaimValidator : AbstractValidator<Claim>
{
    public ClaimValidator()
    {
        RuleFor(c => c.HoursWorked).GreaterThan(0).WithMessage("Hours worked must be greater than zero.");
        RuleFor(c => c.HourlyRate).GreaterThan(0).WithMessage("Hourly rate must be positive.");
        RuleFor(c => c.TotalPayment).GreaterThan(0).WithMessage("Total payment must be positive.");
    }
}
