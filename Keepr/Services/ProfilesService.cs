using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keepr.Services;

public class ProfilesService
{
    // start w/ this, if not working, make own repo
    private readonly ProfilesRepository _repo;

    public ProfilesService(ProfilesRepository repo)
    {
        _repo = repo;
    }

    internal Profile GetById(string id)
    {
        Profile profile = _repo.GetById(id);
        return profile;
    }
}
