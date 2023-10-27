<template>
  <span class="navbar-text">
    <!-- got rid of text-success color for navbar -->
    <button class="btn selectable theme-btn lighten-30  my-2 my-lg-0 theme-font" @click="login"
      v-if="!user.isAuthenticated">
      Login
    </button>
    <div v-else>
      <div v-if="account.id" class="dropdown my-2 my-lg-0">
        <div type="button" class=" border-0 selectable no-select" data-bs-toggle="dropdown" aria-expanded="false">
          <div v-if="account.picture || user.picture" class="account-name theme-font">
            <img :src="account.picture || user.picture" alt="account photo" height="40" class="rounded" />
            {{ account.name.slice(0, account.name.indexOf('@')) }}
          </div>
        </div>
        <div class="dropdown-menu dropdown-menu-lg-end dropdown-menu-start p-0" aria-labelledby="authDropdown">
          <div class="list-group theme-font">
            <router-link :to="{ name: 'Account' }">
              <div class="list-group-item dropdown-item list-group-item-action">
                Manage Account
              </div>
            </router-link>
            <div data-bs-toggle="modal" data-bs-target="#edit-account"
              class="list-group-item dropdown-item list-group-item-action">
              Edit Account
            </div>
            <div class="list-group-item dropdown-item list-group-item-action text-danger selectable" @click="logout">
              <i class="mdi mdi-logout"></i>
              logout
            </div>
          </div>
        </div>
      </div>
    </div>
  </span>
</template>

<script>
import { computed, watchEffect } from 'vue'
import { AppState } from '../AppState'
import { AuthService } from '../services/AuthService'
import { accountService } from '../services/AccountService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';

export default {
  setup() {


    return {
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      async login() {
        AuthService.loginWithPopup()
      },


      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.theme-btn:hover {
  background-color: var(--themeMauve);
  color: var(--themeFadedAmethyst);
}

.account-name {
  color: var(--themeFadedAmethyst);
}

.theme-font {
  font-family: 'Courgette', cursive;
  font-size: 2.5dvh;

}
</style>
