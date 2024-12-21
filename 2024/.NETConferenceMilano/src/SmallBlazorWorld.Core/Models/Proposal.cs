namespace SmallBlazorWorld.Core.Models;

public class Proposal
{
    public Guid Id { get; set; }

    public virtual Talk Talk { get; set; } = default!;

    public virtual Event Event { get; set; } = default!;

    public ProposalState State { get; set; }

    public enum ProposalState
    {
        PendingApproval,
        Approved,
        Rejected
    }
}
