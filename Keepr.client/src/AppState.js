import { reactive } from 'vue'
import { Keep } from './models/Keep.js'
import { Vault } from './models/Vault.js'
import { VaultKeep } from './models/VaultKeep.js'
// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},

  /**@type {Keep[]} */
  keeps: [],
  /**@type {Vault[]} */
  vaults:[],
  /**@type {VaultKeep[]} */
  vaultkeeps: [],
})
