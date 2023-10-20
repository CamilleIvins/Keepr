
namespace Keepr.Controllers;

[ApiController]
[Route("api/keeps")]
public class KeepsController : ControllerBase
{
    private readonly KeepsService _keepsService;
    private readonly Auth0Provider _auth;

    public KeepsController(KeepsService keepsService, Auth0Provider auth)
    {
        _keepsService = keepsService;
        _auth = auth;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Keep>> Create([FromBody] Keep keepData)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            keepData.CreatorId = userInfo.Id;
            Keep newKeep = _keepsService.Create(keepData);
            return newKeep;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public ActionResult<List<Keep>> GetAllKeeps()
    {
        try
        {
            List<Keep> keeps = _keepsService.GetAllKeeps();
            return Ok(keeps);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{keepId}")]
    public ActionResult<Keep> GetKeepById(int keepId)
    {
        try
        {
            Keep keep = _keepsService.GetById(keepId);
            return keep;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [Authorize]
    [HttpPut("{keepId}")]
    public async Task<ActionResult<Keep>> UpdateKeep([FromBody] Keep updateData, int keepId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            updateData.Id = keepId;
            Keep keep = _keepsService.UpdateKeep(updateData, userInfo.Id);
            return Ok(keep);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    // need to add authorizations ✅

    [Authorize]
    [HttpDelete("{keepId}")]
    public async Task<ActionResult<Keep>> ArchiveKeep(int keepId)
    {
        try
        {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            Keep keep = _keepsService.ArchiveKeep(keepId, userInfo.Id);
            return keep;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}

