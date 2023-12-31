import { reactive } from 'vue'
import { Keep } from './models/Keep.js'
import { Vault } from './models/Vault.js'
import { VaultKeep } from './models/Keep.js'
import { Profile } from './models/Account.js'
// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},
  /** @type {import('./models/Account.js').Profile} */
  activeProfile: {},
  
  /** @type {Profile[]} */
  profile: [],
  /**@type {Keep[]} */
  
  keeps: [],
  myKeeps: [],
  profileKeeps: [],
  /**@type {Keep} */
  activeKeep: {},

  /**@type {Vault[]} */
  vaults:[],
  myVaults:[],
  profileVaults:[],
  /**@type {Vault} */
  activeVault:{},

  /**@type {VaultKeep[]} */
  vaultKeeps: [],
  /**@type {VaultKeep} */
  activeVaultKeep: {},
})
