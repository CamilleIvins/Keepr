namespace Keepr.Models;


public class Profile
{
  public string Id { get; set; }
  public string Name { get; set; }
  public string Picture { get; set; }
  public string CoverImg { get; set; }
}

// Account will inherit all these keys
// HelpReviews and Instacult
public class Account : Profile
{
  public string Email { get; set; }

}

// this is a test to see if GitHub sync is working ✅