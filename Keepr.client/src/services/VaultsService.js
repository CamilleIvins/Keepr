import { AppState } from "../AppState.js"
import { api } from "./AxiosService.js"
import { Vault } from "../models/Vault.js"
import { logger } from '../utils/Logger.js';

// TODO - CREATE (post, push) && DELETE (be sure to add Pop confirms on page || keeps, filter out ID OR ID index and splice (index, 1))
class VaultsService {
async createVault(vaultData){
    const res = await api.post(`api/vaults`, vaultData)
    const newVault = new Vault(res.data)
    AppState.vaults.push(newVault)
    return newVault
}

    // ACCOUNT GET
async getVaults(){
    const res = await api.get(`account/vaults`)
    logger.log(res, "get account vaults")
    AppState.myVaults = res.data.map(mV => new Vault(mV))
}

//PROFILE GET
async getProfileVaults(profileId){
const res = await api.get(`api/profiles/${profileId}/vaults`)
logger.log(res.data, "profile service vault GET")
AppState.profileVaults = res.data.map(v=> new Vault(v))
}

//active Vault
async getVaultById(vaultId){
    const res = await api.get(`api/vaults/${vaultId}`)
    AppState.activeVault = new Vault(res.data)
}
}

export const vaultsService = new VaultsService()