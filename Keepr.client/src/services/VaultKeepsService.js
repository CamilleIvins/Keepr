import { AppState } from "../AppState.js"
import { api } from "./AxiosService.js"
import { VaultKeep } from "../models/VaultKeep.js"
import { logger } from '../utils/Logger.js';

// TODO - CREATE && DELETE the Keeps!!!
// NOTE Do NOT create or delete the VK itself
class VaultKeepsService {
async createVK(vaultKeepData){
    const res = await api.post(`api/vaultkeeps`, vaultKeepData)
    logger.log(res)
    let newVK = 
    // if(AppState.vaultkeep.keepId && AppState.vaultkeep.vaultId == )
    AppState.vaultkeeps.push(new VaultKeep(res.data))
}

    async getVaultKeeps(vaultId){
        const res = await api.get(`api/vaults/${vaultId}/keeps`)
        logger.log(res.data)
AppState.vaultkeeps = res.data.map(vk=> new VaultKeep(vk))
    }

    async deleteVK(vaultKeepId){
        const res = await api.delete(`api/vaultkeeps/${vaultKeepId}`)
        let removedVK = AppState.vaultkeeps.findIndex(vk=> vk.id ==vaultKeepId)
        AppState.vaultkeeps.splice(removedVK,1)
        logger.log(res.data,"Keep is removed from Vault")
    }
}

export const vaultKeepsService = new VaultKeepsService()