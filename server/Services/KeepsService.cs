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

    internal Keep GetById(int keepId, string userInfo)
    {
        Keep foundKeep = _repo.GetById(keepId);
        if (foundKeep == null) throw new Exception($"Unable to find Keep with ID:{keepId}");


        this.IncreaseViews(foundKeep);

        return foundKeep;
    }

    internal Keep UpdateKeep(Keep updateData, string userId)
    {
        Keep original = this.GetById(updateData.Id, userId);
        if (original.CreatorId != userId) throw new Exception("unauthorized");
        original.Name = updateData.Name != null ? updateData.Name : original.Name;
        original.Description = updateData.Description != null ? updateData.Description : original.Description;
        original.Img = updateData.Img ?? original.Img;
        original.Views = updateData.Views;
        original.Kept = updateData.Kept;

        _repo.Update(original);
        return original;
    }

    internal Keep ArchiveKeep(int keepId, string userId)
    {
        Keep keep = this.GetById(keepId, userId);
        if (keep.CreatorId != userId) throw new Exception("unauthorized");
        _repo.Delete(keep.Id);
        return keep;
    }

    // PROFILE access
    internal List<Keep> GetProfileKeeps(string profileId)
    {
        List<Keep> pKeeps = _repo.GetProfileKeeps(profileId);
        return pKeeps;
        ;
    }
    internal void IncreaseViews(Keep keep)
    {
        keep.Views++;
        _repo.Update(keep);
    }
}
