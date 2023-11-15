using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/vaults")]
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vaultsService;
        private readonly Auth0Provider _auth;
        private readonly VaultKeepsService _vaultKeepsService;
        private readonly KeepsService _keepsService;

        public VaultsController(VaultsService vaultsService, Auth0Provider auth, VaultKeepsService vaultKeepsService, KeepsService keepsService)
        {
            _vaultsService = vaultsService;
            _auth = auth;
            _vaultKeepsService = vaultKeepsService;
            _keepsService = keepsService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Vault>> Create([FromBody] Vault vaultData)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                vaultData.CreatorId = userInfo.Id;
                Vault newVault = _vaultsService.Create(vaultData);
                return newVault;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<Vault>> GetAllVaults()
        {
            try
            {
                List<Vault> vaults = _vaultsService.GetAllVaults();
                return Ok(vaults);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{vaultId}")]
        public async Task<ActionResult<Vault>> GetVaultById(int vaultId)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                Vault vault = _vaultsService.GetById(vaultId, userInfo?.Id);
                return vault;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // need to make this not visibile if isPrivate is TRUE
        // I think you can do that in front end

        [Authorize]
        [HttpPut("{vaultId}")]
        public async Task<ActionResult<Vault>> UpdateVault([FromBody] Vault updateData, int vaultId)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                updateData.Id = vaultId;
                Vault vault = _vaultsService.UpdateVault(updateData, userInfo.Id);
                return Ok(vault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpDelete("{vaultId}")]
        public async Task<ActionResult<Vault>> ArchiveVault(int vaultId)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
                Vault vault = _vaultsService.CloseVault(vaultId, userInfo.Id);
                return vault;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // VaultKEEPS

        [HttpGet("{vaultId}/keeps")]
        public async Task<ActionResult<List<VaultKeepViewModel>>> GetKeepsByVault(int vaultId)
        {
            try
            {
                Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);

                List<VaultKeepViewModel> vaultKeeps = _vaultKeepsService.GetVKs(vaultId, userInfo?.Id);
                return vaultKeeps;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // THIS REQUIRES THE VIEW MODELS
    }
}