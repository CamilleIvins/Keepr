
namespace Keepr.Models;

public class Keep
{
    public int Id { get; set; }
    public string CreatorId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Views { get; set; }
    public string Img { get; set; }
    public int Kept { get; set; }
    public Profile Creator { get; set; }

}
// Kept is || reportCount inHelpReviews lecture
// virtual properties are returned from tables, not on the table itself