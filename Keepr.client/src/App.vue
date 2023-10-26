<template>
  <body class="bg-img">
    <header class="mb-3">
      <Navbar />
    </header>
    <main style="height: 100%; min-height: 100vh;">
      <router-view />
      <KeepModal />
      <!-- <CreateKeepForm /> -->
      <!-- <CreateVaultForm /> -->
      <!-- <ModalWrapper id='`${id}`' /> -->
      <!-- <template #body>

        </template>
      </ModalWrapper>> -->
    </main>
    <footer class="">
      Made with 💖 and 🦘s
    </footer>
  </body>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import ModalWrapper from './components/ModalWrapper.vue'
import KeepModal from './components/KeepModal.vue'
import CreateKeepForm from './components/CreateKeepForm.vue'
import CreateVaultForm from './components/CreateVaultForm.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, KeepModal }
  // components: { Navbar, KeepModal, CreateKeepForm, CreateVaultForm }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}

// header {
//   position: fixed;
//   top: 0;
//   width: 100%
// }

.bg-img {
  // background-image: url(https://images.unsplash.com/photo-1600172454284-934feca24ccd?auto=format&fit=crop&q=80&w=1335&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D);
  // background-image: url(https://images.unsplash.com/photo-1542374848-4c74196a2207?auto=format&fit=crop&q=80&w=2070&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D);
  background-image: url(https://images.unsplash.com/photo-1568646815107-01383e0a8844?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1887&q=80);
  object-position: center;
  object-fit: contain;
  background-attachment: fixed;

}

footer {
  display: grid;
  place-content: center;
  height: 32px;
  color: var(--themeCharcoal)
}
</style>
