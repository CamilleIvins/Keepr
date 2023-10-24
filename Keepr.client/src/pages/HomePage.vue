<template>
  <section class="container-fluid">
    <section class="my-2 masonry-layout">
      <!-- <section class="row my-2 justify-content-around"> -->
      <!-- <div v-for="keep in keeps" :key="keep.id" class=""> -->

      <div v-for="keep in keeps" :key="keep.id" class="">

        <!-- {{ keeps }} -->
        <KeepCard :keep="keep" />

      </div>
    </section>
  </section>
</template>

<script>
import { computed, onMounted } from 'vue';
import { keepsService } from '../services/KeepsService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { AppState } from "../AppState.js"
import { Keep } from '../models/Keep.js';
import KeepCard from '../components/KeepCard.vue';
// import ModalWrapper from './ModalWrapper.vue';



export default {
  setup() {
    onMounted(() => {
      getKeeps();
    });
    async function getKeeps() {
      try {
        await keepsService.getKeeps();
        logger.log("HomePage Keep GET");
      }
      catch (error) {
        Pop.error(error);
      }
    }
    return {
      // computed
      keeps: computed(() => AppState.keeps),
    };
  },
  components: { KeepCard }
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

@media screen and (max-width: 768px) {
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
</style>
