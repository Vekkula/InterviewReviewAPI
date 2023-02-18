namespace InterviewReviewAPI.Models;
public class InterviewProcessDTO
{
    public long Id { get; set; }
    public string? companyName { get; set; }
    public string? JobName { get; set; }
    public string? Description { get; set; }
    public string? WhereDiscovered { get; set; }
    public string? Review { get; set; }
    public bool IsComplete { get; set; }
    public DateTime ApplyDate { get; set; }
    public DateTime? FirstContact { get; set; }
    public DateTime? EndDate { get; set; }
    public int InterviewCount { get; set; }
    public bool OnlineAssesment { get; set; }
    public bool VideoInterview { get; set; }
    public bool JobOffered { get; set; }
    public bool OfferAccepted { get; set; }
    public string? Secret { get; set; }
}
