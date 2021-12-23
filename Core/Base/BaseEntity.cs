namespace Core.Base;

public class BaseEntity
{
    public long Id { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public bool? Deleted { get; set; }
    public string? DeletedBy { get; set; }
}