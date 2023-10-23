using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/profiles")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _profilesService;
        private readonly KeepsService _keepsService;
        private readonly VaultsService _vaultsService;
        private readonly Auth0Provider _auth;

        public ProfilesController(KeepsService keepsService, VaultsService vaultsService, Auth0Provider auth, ProfilesService profilesService)
        {
            _keepsService = keepsService;
            _vaultsService = vaultsService;
            _auth = auth;
            _profilesService = profilesService;
        }

        [HttpGet("{profileId}")]
        public ActionResult<Profile> GetProfile(string id)
        {
            try
            {
                // Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                Profile profile = _profilesService.GetById(id);
                return Ok(profile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // match Postman endpoint EXACTLY profileId, NOT id
    }
}
