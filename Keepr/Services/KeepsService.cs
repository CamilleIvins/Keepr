using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keepr.Controllers;

namespace Keepr.Services;

public class KeepsService
{
    private readonly KeepsRepository _repo;
    private readonly VaultsService _vaultsService;
    public KeepsService(KeepsRepository repo, VaultsService vaultsService)
    {
        _repo = repo;
        _vaultsService = vaultsService;
    }

    internal Keep Create(Keep keepData)
    {
        Keep newKeep = _repo.Create(keepData);
        return newKeep;
    }

    internal List<Keep> GetAllKeeps()
    {
        List<Keep> keeps = _repo.Get();
        return keeps;
    }

    internal Keep GetById(int keepId)
    {
        Keep foundKeep = _repo.GetById(keepId);
        if (foundKeep == null) throw new Exception($"Unable to find Keep with ID:{keepId}");
        return foundKeep;
    }

    internal Keep UpdateKeep(Keep updateData, string userId)
    {
        Keep original = this.GetById(updateData.Id);
        if (original.CreatorId != userId) throw new Exception("unauthorized");
        original.Name = updateData.Name != null ? updateData.Name : original.Name;
        original.Description = updateData.Description != null ? updateData.Description : original.Description;
        original.Img = updateData.Img ?? original.Img;
        _repo.Update(original);
        return original;
    }

    internal Keep ArchiveKeep(int keepId, string userId)
    {
        Keep keep = this.GetById(keepId);
        if (keep.CreatorId != userId) throw new Exception("unauthorized");
        _repo.Delete(keep.Id);
        return keep;
    }

}
