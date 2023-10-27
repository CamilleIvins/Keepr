import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { Vault } from '../models/Vault.js'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

// TODO - allow to edit account, button on page
class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

async editAccount(updateData){
  
    const res = await api.put(`/account`, updateData)
    logger.log(res.data)
    AppState.account = new Account(res.data)

}

// ⬇️ MOVED to AUTHservice
  // async getMyVaults(){
   
  //     const res = await api.get(`/account/vaults`)
  //     logger.log(res, "my vaults")
  //     AppState.myVaults = res.data.map(v=>new Vault(v))
    
  // }
}

export const accountService = new AccountService()
