export class VaultKeep {
    constructor(data)
    {
        this.creatorId = data.creatorId
        this.vaultId = data.vaultId
        this.keepId = data.keepId
    }
}

// just relational, no "new X (data.y)"