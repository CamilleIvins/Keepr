<template>
  <section class="container-fluid" v-if="account.id">
    <section class="row">
      <div class="about text-center mt-3 px-0">
        <img :src="account.coverImg" alt="" class="account-cover position-absolute">
        <div class="welcome font-dancing">Welcome, {{ account.name.slice(0, account.name.indexOf('@')) }}</div>
        <!-- <div class="text-center"> -->
        <img class="rounded-circle profile-pic mb-5" :src="account.picture" alt="" />
        <!-- </div> -->
        <div class="text-center mt-5">
        </div>
      </div>
      <div class=" text-center mt-5">
        <p class="mt-md-5 mt-2">{{ myVaults.length }}Vaults||{{ myKeeps.length }}Keeps</p>
        <p class="mt-2">{{ account.email }}</p>
      </div>


      <hr class="mt-3">
      <section class="row">
        <button class="btn theme-btn theme-font col-md-2 col-6 ms-2" data-bs-toggle="modal"
          data-bs-target="#create-vault">
          Create a Vault

        </button>
        <p>My Vaults</p>
        <div v-for="vault in myVaults" :key="vault.id" class="col-md-3 col-6 g-3">
          <!-- {{ vault }} -->
          <VaultCard :vault="vault" />
        </div>
      </section>
      <p>My Keeps</p>
      <section class="my-2 masonry-layout">
        <!-- add masonry -->
        <div v-for="keep in myKeeps" :key="keep.id" class="">
          <!-- {{ keep }} -->
          <KeepCard :keep="keep" />
        </div>
      </section>

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
import { keepsService } from '../services/KeepsService.js';
import Pop from '../utils/Pop.js';
import VaultCard from '../components/VaultCard.vue';
import KeepCard from '../components/KeepCard.vue';
import CreateVaultForm from '../components/CreateVaultForm.vue';
import { logger } from '../utils/Logger.js';
import { useRoute } from 'vue-router';

export default {
  setup() {

    onMounted(() => {
      getMyKeeps()
      AppState.myVaults
    })
    const editable = ref({})
    watchEffect(() => {
      editable.value = { ...AppState.account }
    });

    // async function getMyVaults() {
    //   try {
    //     await accountService.getMyVaults();
    //     logger.log("account page vault GET")
    //   } catch (error) {
    //     Pop.error(error);
    //   }
    // }
    async function getMyKeeps() {
      try {
        const profileId = AppState.account.id
        await keepsService.getCreatorKeeps(profileId);
        logger.log("account page Keep GET")
      } catch (error) {
        Pop.error(error);
      }
    }

    return {
      editable,
      account: computed(() => AppState.account),
      myVaults: computed(() => AppState.myVaults),
      myKeeps: computed(() => AppState.myKeeps)
    }
  },
  components: { VaultCard, KeepCard }
}
</script>

<style lang="scss" scoped>
/* img {
  max-width: 100px;
} */

.welcome {
  position: relative;
  text-align: center;
  bottom: -25%;
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
  right: 0;
  /* elevation-2 settings */
  box-shadow: 0 3px 3px -1px rgba(205, 205, 205, 0.2),
    0 5px 6px 0 rgba(205, 205, 205, 0.14),
    0 1px 8px 0 rgba(205, 205, 205, 0.12);
}

.theme-btn:hover {
  box-shadow: 0 3px 3px -1px #2d2d2d3b,
    0 5px 6px 0 #2d2d2d23,
    0 1px 8px 0 #2d2d2d17;
  transform: scale(1.02);
  transition: 0.35s;
}

.theme-btn {
  background-color: var(--themeFadedAmethyst);
  color: var(--themeCream);
}

@media screen and (min-width: 768px) {
  img.account-cover {
    height: 35vh;
    width: 100dvw;
    object-fit: cover;
    object-position: center;
    right: 0;
    /* elevation-2 settings */
    box-shadow: 0 3px 3px -1px rgba(205, 205, 205, 0.2),
      0 5px 6px 0 rgba(205, 205, 205, 0.14),
      0 1px 8px 0 rgba(205, 205, 205, 0.12);
  }

  .masonry-layout {
    $gap: 1.25em;
    columns: 2;
    column-gap: $gap;

    div {
      // max-height: 30dvh;
      // min-height: 15dvh;
      width: 100%;
      margin-bottom: $gap;
    }
  }
}

.profile-pic {
  height: 20dvh;
  position: relative;
  bottom: -18vh;
  filter: drop-shadow(0 0 8px #292828);
  /* box-shadow: 0 3px 3px -1px rgba(205, 205, 205, 0.2),
    0 5px 6px 0 rgba(205, 205, 205, 0.14),
    0 1px 8px 0 rgba(205, 205, 205, 0.12); */
  box-shadow: 0 -0.5px 8px -0.5px #ffffff,
    0 -1px 8px -2px #ffffff;
}

.masonry-layout {
  $gap: 1.25em;
  columns: 4;
  column-gap: $gap;

  div {
    // max-height: 30dvh;
    // min-height: 15dvh;
    width: 100%;
    margin-bottom: $gap;
  }
}
</style>
