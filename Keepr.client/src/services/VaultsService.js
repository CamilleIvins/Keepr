import { AppState } from "../AppState.js"
import { api } from "./AxiosService.js"
import { Vault } from "../models/Vault.js"
import { logger } from '../utils/Logger.js';


class VaultsService {
async getVaults(){
    const res = await api.get(`account/vaults`)
    logger.log(res, "get account vaults")
    AppState.myVaults = res.data.map(mV => new Vault(mV))
}

async getProfileVaults(profileId){
const res = await api.get(`api/profiles/${profileId}/vaults`)
logger.log(res.data, "profile service vault GET")
AppState.profileVaults = res.data.map(v=> new Vault(v))
}

async getVaultById(vaultId){
    const res = await api.get(`api/vaults/${vaultId}`)
    AppState.activeVault = new Vault(res.data)
}
}

export const vaultsService = new VaultsService()