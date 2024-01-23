import { AppState } from "../AppState.js"
import { api } from "./AxiosService.js"
// import { VaultKeep } from "../models/VaultKeep.js"
import { VaultKeep } from "../models/Keep.js";
import { logger } from '../utils/Logger.js';

// TODO - CREATE && DELETE the Keeps!!!
// NOTE Do NOT create or delete the VK itself
class VaultKeepsService {
async createVK(vaultKeepData){
    const res = await api.post(`api/vaultkeeps`, vaultKeepData)
    logger.log(res.data, "Keep added to Vault")
    const newVK = new VaultKeep(res.data)
    AppState.activeVaultKeep = newVK
    // if(AppState.user == newVK.creatorId)
    // {
    //     newVK.kept++
    //     api.put()
    // }
    // if(AppState.vaultkeep.keepId && AppState.vaultkeep.vaultId == )
    AppState.vaultKeeps.push(newVK)
    
}

    async getVaultKeeps(vaultId){
        const res = await api.get(`api/vaults/${vaultId}/keeps`)
        logger.log(res.data, "Retrieving Keeps for this Vault")
AppState.vaultKeeps = res.data.map(vk=> new VaultKeep(vk))
    }

    async deleteVK(vaultKeepId){
        const res = await api.delete(`api/vaultkeeps/${vaultKeepId}`)
        // let removedVK = AppState.vaultkeeps.findIndex(vk=> vk.id ==vaultKeepId)
        // AppState.vaultkeeps.splice(removedVK,1)
        AppState.vaultKeeps = AppState.vaultKeeps.filter(vk => vk.vaultKeepId!=vaultKeepId)
        logger.log(res.data,"Keep is removed from Vault")
    }
}

export const vaultKeepsService = new VaultKeepsService()