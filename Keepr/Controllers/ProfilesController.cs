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
        private readonly AccountService _accountService;
        private readonly KeepsService _keepsService;
        private readonly VaultsService _vaultsService;
        private readonly Auth0Provider _auth;

        public ProfilesController(KeepsService keepsService, VaultsService vaultsService, Auth0Provider auth, AccountService accountService)
        {
            _keepsService = keepsService;
            _vaultsService = vaultsService;
            _auth = auth;
            _accountService = accountService;
        }

        [HttpGet("{id}")]
        public ActionResult<Profile> GetProfile(string id)
        {
            try
            {
                Profile profile = _accountService.GetById(id);
                return Ok(profile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
