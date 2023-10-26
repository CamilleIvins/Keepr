import { AppState } from "../AppState.js"
import { api } from "./AxiosService.js"
import { VaultKeep } from "../models/VaultKeep.js"
import { logger } from '../utils/Logger.js';

// TODO - CREATE && DELETE the Keeps!!!
// NOTE Do NOT create or delete the VK itself
class VaultKeepsService {


    async getVaultKeeps(vaultId){
        const res = await api.get(`api/vaults/${vaultId}/keeps`)
        logger.log(res.data)
AppState.vaultkeeps = res.data.map(vk=> new VaultKeep(vk))
    }
}

export const vaultKeepsService = new VaultKeepsService()