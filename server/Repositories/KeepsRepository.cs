using System;
using System.Collections.Generic;
using System.Linq;
using Keepr.Interfaces;

namespace Keepr.Repositories;

public class KeepsRepository
{

    private readonly IDbConnection _db;
    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }
    public Keep Create(Keep keepData)
    {
        string sql = @"
        INSERT INTO keeps
        (name, description, img, views, kept, creatorId)
        VALUES
        (@name, @description, @img, @views, @kept, @creatorId);

        SELECT
        acct.*,
        keeps.*
        FROM keeps
        JOIN accounts acct ON acct.id = keeps.creatorId
        WHERE keeps.id = LAST_INSERT_ID()
        ;";
        Keep newKeep = _db.Query<Account, Keep, Keep>(sql, (account, keep) =>
        {
            keep.Creator = account;
            return keep;
        }, keepData).FirstOrDefault();
        return newKeep;
    }

    //Get functions as a GetAllKeeps
    public List<Keep> Get()
    {
        string sql = @"
        SELECT
        keeps.*,
        acct.*
        FROM keeps
        JOIN accounts acct ON acct.id = keeps.creatorId
        ;";
        List<Keep> keeps = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
        {
            keep.Creator = account;
            return keep;
        }).ToList();
        return keeps;
    }

    public Keep GetById(int keepId)
    {
        string sql = @"
        SELECT
        keeps.*,
        acct.*
        FROM keeps
        JOIN accounts acct ON keeps.creatorId = acct.id
        WHERE keeps.id = @keepId
        ;";
        Keep foundKeep = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
        {
            keep.Creator = account;
            return keep;
        }, new { keepId }).FirstOrDefault();
        return foundKeep;
    }
    // @keepId - remember it is singular!!!✅
    // GET BY VAULT || Pictures in Album


    public void Update(Keep updateData)
    {
        string sql = @"
        UPDATE  keeps
        SET
        name = @name,
        description = @description,
        img = @img,
        views = @views,
        kept = @kept
        WHERE id = @id
        ;";
        _db.Execute(sql, updateData);
        // Keep keep = _db.Query<Keep>(sql, keepData).FirstOrDefault();
        // return keep;
    }

    public void Delete(int id)
    {
        string sql = @"
       DELETE FROM keeps
       WHERE id = @id
       LIMIT 1
       ;";
        _db.Execute(sql, new { id });
    }
    // @id matches the parameter, so NOT @keepId

    // PROFILE keeps
    internal List<Keep> GetProfileKeeps(string profileId)
    {
        string sql = @"
        SELECT
        keeps.*,
        profile.*
        FROM keeps
        Join accounts profile ON profile.id = keeps.creatorId
        WHERE keeps.creatorId = @profileId
        ;";
        List<Keep> pKeeps = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            // ⬇️ this is the line that will cause seizure-speed errors
            // keep.CreatorId = profile.Id;
            return keep;
        }, new { profileId }).ToList();
        return pKeeps;
    }
    // WHERE keeps.creatorId = @profileId????


}
