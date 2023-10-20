using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keepr.Services;

public class VaultsService
{
    private readonly VaultsRepository _repo;
    public VaultsService(VaultsRepository repo)
    {
        _repo = repo;
    }

    internal Vault Create(Vault vaultData)
    {
        Vault newVault = _repo.Create(vaultData);
        return newVault;
    }

    internal List<Vault> GetAllVaults()
    {
        List<Vault> vaults = _repo.Get();
        return vaults;
    }

    internal Vault GetById(int vaultId, string userId)
    {
        Vault foundVault = _repo.GetById(vaultId);
        if (foundVault == null) throw new Exception($"Unable to find Vault with ID:{vaultId}");
        if (foundVault.IsPrivate == true && foundVault.CreatorId != userId) throw new Exception($"Unable to find Vault with ID:{foundVault.Name}");
        return foundVault;
    }

    internal Vault UpdateVault(Vault updateData, string userId)
    {
        Vault original = this.GetById(updateData.Id, userId);
        if (original.CreatorId != userId) throw new Exception("unauthorized");
        original.Name = updateData.Name != null ? updateData.Name : original.Name;
        original.Description = updateData.Description != null ? updateData.Description : original.Description;
        original.Img = updateData.Img ?? original.Img;
        original.IsPrivate = updateData.IsPrivate ?? original.IsPrivate;
        _repo.Update(original);
        return original;
    }

    internal Vault CloseVault(int vaultId, string userId)
    {
        Vault vault = this.GetById(vaultId, userId);
        if (vault.CreatorId != userId) throw new Exception("unauthorized");
        _repo.Delete(vault.Id);
        return vault;
    }
}
