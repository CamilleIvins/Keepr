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
}
