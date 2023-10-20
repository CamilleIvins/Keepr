using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keepr.Services;

public class KeepsService
{
    private readonly KeepsRepository _repo;
    public KeepsService(KeepsRepository repo)
    {
        _repo = repo;
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

    internal Keep UpdateKeep(Keep updateData)
    {
        Keep original = this.GetById(updateData.Id);
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
