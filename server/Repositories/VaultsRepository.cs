using System;
using System.Collections.Generic;
using System.Linq;
using Keepr.Interfaces;


namespace Keepr.Repositories;

public class VaultsRepository : IRepository<Vault, int>
{

    private readonly IDbConnection _db;
    public VaultsRepository(IDbConnection db)
    {
        _db = db;
    }
    public Vault Create(Vault vaultData)
    {
        string sql = @"
        INSERT INTO vaults
        (name, description, img, isPrivate, creatorId)
        VALUES
        (@name, @description, @img, @isPrivate, @creatorId);

        SELECT
        acct.*,
        vaults.*
        FROM vaults
        JOIN accounts acct ON acct.id = vaults.creatorId
        WHERE vaults.id = LAST_INSERT_ID()
        ;";
        Vault vault = _db.Query<Account, Vault, Vault>(sql, (account, vault) =>
        {
            vault.Creator = account;
            return vault;
        }, vaultData).FirstOrDefault();
        return vault;
    }

    public List<Vault> Get()
    {
        string sql = @"
        SELECT
        vaults.*,
        acct.*
        FROM vaults
        JOIN accounts acct ON acct.id = vaults.creatorId
        ;";
        List<Vault> vaults = _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
        {
            vault.Creator = account;
            return vault;
        }).ToList();
        return vaults;
    }
    // FIXME ⬇️ swapped the join order, maybe this will fix Postman error???
    public Vault GetById(int vaultId)
    {
        string sql = @"
        SELECT
        vaults.*,
        acct.*
        FROM vaults
        JOIN accounts acct ON acct.id = vaults.creatorId
        WHERE vaults.id = @vaultId
        ;";
        Vault foundVault = _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
        {
            vault.Creator = account;
            return vault;
        }, new { vaultId }).FirstOrDefault();
        return foundVault;
    }

    public void Update(Vault updateData)
    {
        string sql = @"
        UPDATE  vaults
        SET
        name = @name,
        description = @description,
        img = @img,
        isPrivate = @isPrivate
        WHERE id = @id
        ;";
        _db.Execute(sql, updateData);
    }

    public void Delete(int id)
    {
        string sql = @"
       DELETE FROM vaults
       WHERE id = @id
       LIMIT 1
       ;";
        _db.Execute(sql, new { id });
    }
    // Accounts-based GET
    internal List<Vault> GetByCreator(string userId)
    {
        string sql = @"
        SELECT
        vaults.*,
        acct.*
        FROM vaults
        JOIN accounts acct ON acct.id = vaults.creatorId
        WHERE vaults.creatorId = @userId
        ;";
        List<Vault> myVaults = _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
        {
            vault.Creator = account;
            return vault;
        }, new { userId }).ToList();
        return myVaults;
    }

    internal List<Vault> GetProfileVaults(string profileId)
    {
        string sql = @"
        SELECT
        vaults.*,
        profile.*
        FROM vaults
        Join accounts profile ON profile.id = vaults.creatorId
        WHERE vaults.creatorId = @profileId
        ;";
        List<Vault> pVaults = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
        {
            vault.Creator = profile;
            // vault.CreatorId = profile.Id;
            return vault;
        }, new { profileId }).ToList();
        return pVaults;
    }
}
