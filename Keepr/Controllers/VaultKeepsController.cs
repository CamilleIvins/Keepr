using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/vaultkeeps")]
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vaultKeepsService;


        private readonly Auth0Provider _auth;
        public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth)
        {
            _vaultKeepsService = vaultKeepsService;
            _auth = auth;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep vaultKeepsData)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                VaultKeep newVaultKeeps = _vaultKeepsService.Create(vaultKeepsData, userInfo);
                return newVaultKeeps;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{vaultKeepId}")]
        [Authorize]
        public async Task<ActionResult<VaultKeep>> DeleteVK(int vaultKeepId)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                VaultKeep vk = _vaultKeepsService.DeleteVK(vaultKeepId, userInfo.Id);
                return vk;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}