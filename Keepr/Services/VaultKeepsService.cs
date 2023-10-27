using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keepr.Services;

public class VaultKeepsService
{
    private readonly VaultKeepsRepository _repo;
    private readonly KeepsRepository _keepsRepo;
    private readonly VaultsService _vaultsService;
    private readonly KeepsService _keepsService;
    public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService, KeepsService keepsService, KeepsRepository keepsRepo)
    {
        _repo = repo;
        _vaultsService = vaultsService;
        _keepsService = keepsService;
        _keepsRepo = keepsRepo;
    }
    // FIXME - and Postman Test!!!!!
    internal VaultKeep Create(VaultKeep vaultKeepsData, Account userInfo)
    {
        Vault vault = _vaultsService.GetById(vaultKeepsData.VaultId, vaultKeepsData.CreatorId);
        // if (userInfo == null) throw new Exception("Unauthorized");
        if (vault.CreatorId != vaultKeepsData.CreatorId) throw new Exception("Unauthorized");
        // vaultKeepsData.CreatorId = userInfo.Id;
        VaultKeep newVK = _repo.Create(vaultKeepsData);
        Keep keep = _keepsService.GetById(vaultKeepsData.KeepId);
        keep.Kept++;
        _keepsRepo.Update(keep);
        // if (keep.Id != vaultKeepsData.KeepId) throw new Exception("Error, no keep at this ID");
        // else
        // {
        //     keep.Kept++;

        // }
        return newVK;

        // if (userInfo.Id == null)
        // {
        //     throw new Exception("Unauthorized");
        // }
        // else
        // {
        //     vaultKeepsData.CreatorId = userInfo.Id;
        //     VaultKeep newVK = _repo.Create(vaultKeepsData);
        //     return newVK;
        // }
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


    internal List<VaultKeepViewModel> GetVKs(int vaultId, string userId)
    {
        Vault vault = _vaultsService.GetById(vaultId, userId);
        List<VaultKeepViewModel> vKs = _repo.GetVKs(vaultId);

        return vKs;
    }

    internal VaultKeep DeleteVK(int vaultKeepId, string userId)
    {
        VaultKeep vk = this.GetVKById(vaultKeepId);
        if (vk.CreatorId != userId) throw new Exception("Unauthorized");
        _repo.Delete(vk.Id);
        return vk;
    }

}
