public class Claim
{
    public int Id { get; set; }
    public int LecturerId { get; set; }
    public double HoursWorked { get; set; }
    public double HourlyRate { get; set; }
    public double TotalPayment { get; set; }
    public DateTime SubmissionDate { get; set; }
    public string Status { get; set; }
}
