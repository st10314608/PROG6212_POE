public class LecturerValidator : AbstractValidator<Lecturer>
{
    public LecturerValidator()
    {
        RuleFor(l => l.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(l => l.Email).EmailAddress().WithMessage("Invalid email address.");
        RuleFor(l => l.Department).NotEmpty().WithMessage("Department is required.");
    }
}
