using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keepr.Interfaces;

namespace Keepr.Repositories;

public class VaultKeepsRepository
{
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db)
    {
        _db = db;
    }
    public VaultKeep Create(VaultKeep vaultKeepData)
    {
        string sql = @"
        INSERT INTO vaultKeeps
        (creatorId, vaultId, keepId)
        VALUES
        (@creatorId, @vaultId, @keepId);

        SELECT LAST_INSERT_ID()
        ;";
        int lastInsertId = _db.ExecuteScalar<int>(sql, vaultKeepData);
        vaultKeepData.Id = lastInsertId;
        return vaultKeepData;
    }

    public List<VaultKeep> GetVKByVault(int vaultId)
    {
        throw new NotImplementedException();
    }

    public VaultKeep GetById(int vaultKeepId)
    {
        string sql = @"
        SELECT
        vk.*,
        keeps.*
        FROM vaultKeeps vk
        JOIN keeps on vk.keepId = keeps.id
        WHERE vk.id = @vaultKeepId
        ;";
        VaultKeep foundVaultKeep = _db.Query<VaultKeep, Keep, VaultKeep>(sql, (vaultKeep, keep) =>
        {
            vaultKeep.KeepId = keep.Id;
            return vaultKeep;
        }, new { vaultKeepId }).FirstOrDefault();
        return foundVaultKeep;
    }

    public void Delete(int id)
    {
        string sql = @"
       DELETE FROM vaultKeeps
       WHERE id = @id
       LIMIT 1
       ;";
        _db.Execute(sql, new { id });
    }

    internal List<VaultKeepViewModel> GetVKs(int vaultId)
    {
        throw new Exception();
    }
    // internal List<VaultKeepViewModel> GetVKs(int vaultId)
    // {
    //     string sql = @"
    //     SELECT
    //     vks.*,
    //     vaults.*,
    //     keeps.*,
    //     keepVault.*

    //     FROM vaultKeeps vks
    //         JOIN vaults ON vaults.id = vks.vaultId
    //         JOIN keeps ON keeps.id = vks.keepId
    //         JOIN keeps keepVault ON keepVault.vaultKeepId = vault.Id
    //         WHERE vks.vaultId = @vaultId
    //     ;";
    //     List<VaultKeepViewModel> vaultKeeps = _db.Query<VaultKeep, Vault, VaultKeepViewModel, Vault, VaultKeep>(sql, (vk, vault, keep, keepHome) =>
    //     {
    //         vk.VaultId = vault.Id;
    //         keep.VaultKeepId = keepHome.Id;
    //         vk.KeepId = keep.Id;
    //         // model.VaultKeepId = vk.VaultId;
    //         // model.Id = vk.KeepId;
    //         // vk.VaultId = vault.Id;

    //         // vk.KeepId = model.Id;
    //         // vk.VaultId = model.VaultKeepId;
    //         return vk;
    //     }, new { vaultId }).ToList();
    //     return vaultKeeps;
    // }
    // {
    //     string sql = @"
    //     SELECT
    //     vks.*,
    //     keeps.*,
    //     vaults.*
    //     FROM vaultKeeps vks
    //         JOIN keeps ON keeps.id = vks.keepId
    //         JOIN vaults ON vaults.id = vks.keepId
    //         WHERE vks.vaultId = @vaultId
    //     ;";
    //     List<VaultKeepViewModel> vaultKeeps = _db.Query<VaultKeep, VaultKeepViewModel, VaultKeep>(sql, (vk, model) =>
    //     {
    //         model.VaultKeepId = vk.Id;
    //         model.Id = vk.KeepId;
    //         // vk.VaultId = vault.Id;

    //         // vk.KeepId = model.Id;
    //         // vk.VaultId = model.VaultKeepId;
    //         return vk;
    //     }, new { vaultId }).ToList();
    //     return vaultKeeps;
    // }
}
