<template>
  <section class="container-fluid" v-if="account.id">
    <section class="row">
      <div class="about text-center px-0">
        <img :src="account.coverImg" alt="" class="account-cover">
        <div class="welcome font-dancing">Welcome {{ account.name.slice(0, account.name.indexOf('@')) }}</div>
        <!-- <div class="text-center"> -->
        <img class="rounded-circle profile-pic" :src="account.picture" alt="" />
        <!-- </div> -->
        <p>{{ account.email }}</p>
      </div>
      <p>My Vaults</p>
      <p>{{ vault.name }}</p>
      <hr>


    </section>
  </section>

  <!-- <section class="container-fluid">
    <section class="row">
      <div class="">
        <img :src="account.coverImg" alt="" class="elevation-4 account-cover">
        <div class="welcome">Welcome {{ account.name.slice(0, account.name.indexOf('@')) }}</div>
      </div>
    </section>
  </section> -->
</template>

<script>
import { computed, onMounted, ref, watchEffect } from 'vue';
import { AppState } from '../AppState';
import { accountService } from '../services/AccountService.js';
import Pop from '../utils/Pop.js';
export default {
  setup() {
    onMounted(() => {
      getMyVaults()
    })
    const editable = ref({})
    watchEffect(() => {
      editable.value = { ...AppState.account }
    });

    async function getMyVaults() {
      try {
        await accountService.getMyVaults();
      } catch (error) {
        Pop.error(error);
      }
    }

    return {
      editable,
      account: computed(() => AppState.account),
      vault: computed(() => AppState.vaults.find(owner => owner.creatorId == AppState.account.id))
    }
  }
}
</script>

<style scoped>
/* img {
  max-width: 100px;
} */

.welcome {
  position: relative;
  text-align: center;
  bottom: 35%;
  filter: drop-shadow(3px 8px 7px #292828);
  color: aliceblue;
  font-size: xxx-large;
  text-shadow: .5px .5px 1px black;


}

img.account-cover {
  height: 35dvh;
  width: 100%;
  object-fit: cover;
  object-position: center;
  /* elevation-2 settings */
  box-shadow: 0 3px 3px -1px rgba(205, 205, 205, 0.2),
    0 5px 6px 0 rgba(205, 205, 205, 0.14),
    0 1px 8px 0 rgba(205, 205, 205, 0.12);
}

@media screen and (min-width: 768px) {
  img.account-cover {
    height: 35vh;
    width: 100dvw;
    object-fit: cover;
    object-position: center;
    /* elevation-2 settings */
    box-shadow: 0 3px 3px -1px rgba(205, 205, 205, 0.2),
      0 5px 6px 0 rgba(205, 205, 205, 0.14),
      0 1px 8px 0 rgba(205, 205, 205, 0.12);
  }
}

.profile-pic {
  height: 20dvh;
  position: relative;
  bottom: 15vh;
  filter: drop-shadow(0 0 8px #292828);
  /* box-shadow: 0 3px 3px -1px rgba(205, 205, 205, 0.2),
    0 5px 6px 0 rgba(205, 205, 205, 0.14),
    0 1px 8px 0 rgba(205, 205, 205, 0.12); */
  box-shadow: 0 -0.5px 8px -0.5px #ffffff,
    0 -1px 8px -2px #ffffff;
}
</style>
