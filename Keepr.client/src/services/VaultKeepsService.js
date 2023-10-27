import { AppState } from "../AppState.js"
import { api } from "./AxiosService.js"
import { VaultKeep } from "../models/VaultKeep.js"
import { logger } from '../utils/Logger.js';

// TODO - CREATE && DELETE the Keeps!!!
// NOTE Do NOT create or delete the VK itself
class VaultKeepsService {
async createVK(vaultKeepData){
    const res = await api.post(`api/vaultkeeps`, vaultKeepData)
    logger.log(res.data, "Keep added to Vault")
    const newVK = new VaultKeep(res.data)
    // if(AppState.vaultkeep.keepId && AppState.vaultkeep.vaultId == )
    AppState.vaultkeeps.push(newVK)
    newVK.kept++
}

    async getVaultKeeps(vaultId){
        const res = await api.get(`api/vaults/${vaultId}/keeps`)
        logger.log(res.data, "Retrieving Keeps for this Vault")
AppState.vaultkeeps = res.data.map(vk=> new VaultKeep(vk))
    }

    async deleteVK(vaultKeepId){
        const res = await api.delete(`api/vaultkeeps/${vaultKeepId}`)
        // let removedVK = AppState.vaultkeeps.findIndex(vk=> vk.id ==vaultKeepId)
        // AppState.vaultkeeps.splice(removedVK,1)
        AppState.vaultkeeps = AppState.vaultkeeps.filter(vk => vk.vaultKeepId!=vaultKeepId)
        logger.log(res.data,"Keep is removed from Vault")
    }
}

export const vaultKeepsService = new VaultKeepsService()