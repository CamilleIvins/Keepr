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
        public ActionResult<Profile> GetProfile(string profileId)
        {
            try
            {
                // Profile userInfo = await _auth.GetUserInfoAsync<Profile>(HttpContext);
                Profile profile = _profilesService.GetById(profileId);
                return Ok(profile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // match Postman endpoint EXACTLY profileId, NOT id

        [HttpGet("{profileId}/keeps")]
        public ActionResult<List<Keep>> GetProfileKeeps(string profileId)
        {
            try
            {
                // Profile profile = _profilesService.GetById(profileId);

                List<Keep> pKeeps = _keepsService.GetProfileKeeps(profileId);
                return Ok(pKeeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{profileId}/vaults")]
        public async Task<ActionResult<List<Vault>>> GetProfileVaults(string profileId)
        {
            try
            {
                Profile userInfo = await _auth.GetUserInfoAsync<Profile>(HttpContext);

                List<Vault> pVaults = _vaultsService.GetProfileVaults(profileId, userInfo?.Id);
                return Ok(pVaults);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



    }
}
