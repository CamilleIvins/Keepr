using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keepr.Services;

public class VaultKeepsService
{
    private readonly VaultKeepsRepository _repo;
    private readonly VaultsService _vaultsService;
    public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService)
    {
        _repo = repo;
        _vaultsService = vaultsService;
    }

    internal VaultKeep Create(VaultKeep vaultKeepsData, Account userInfo)
    {
        vaultKeepsData.CreatorId = userInfo.Id;
        VaultKeep newVK = _repo.Create(vaultKeepsData);
        return newVK;
    }

    internal VaultKeep GetVKById(int vaultKeepId)
    {
        VaultKeep vaultKeep = _repo.GetById(vaultKeepId);
        if (vaultKeep == null) throw new Exception("VaultKeep ID not found");
        return vaultKeep;
    }

    // NEED  a GetById for below to work???? <-- these can just exist here
    // internal List<VaultKeepViewModel> GetVKs(int vaultId, string userId)
    // {
    //     _vaultsService.GetById(vaultId, userId);
    //     List<VaultKeepViewModel> vaultKeeps = _repo.GetVKByVault(vaultId);
    //     return vaultKeeps;
    // }
}
