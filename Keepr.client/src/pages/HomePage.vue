<template>
  <section class="row my-2 justify-content-around">
    <div class="col-md-3 col-6 px-2 py-1">
      {{ keeps.name }}
    </div>
  </section>
</template>

<script>
import { computed, onMounted } from 'vue';
import { keepsService } from '../services/KeepsService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { AppState } from "../AppState.js"


export default {
  setup() {
    onMounted(() => {
      getKeeps();
    });

    async function getKeeps() {
      try {
        await keepsService.getKeeps()
        logger.log("HomePage Keep GET")
      } catch (error) {
        Pop.error(error)
      }
    }
    return {
      // computed
      keeps: computed(() => AppState.keeps),
    }
  }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: 50vw;

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
