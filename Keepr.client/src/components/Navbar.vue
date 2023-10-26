<template>
  <nav class="navbar navbar-expand-lg navbar-dark px-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <!-- <img alt="logo" src="../assets/img/cw-logo.png" height="45" /> -->
        <img class="logo" alt="logo"
          src="https://media1.giphy.com/media/v1.Y2lkPTc5MGI3NjExOXpwcWxoYTJoNXd1ank4MWJzYjk2bnIyeTJrOHRzNG15bW11ZWZyMyZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9cw/1l9wPn8viAMWCVaCTw/giphy.gif"
          height="55" />
        <!-- <div src="https://giphy.com/embed/1l9wPn8viAMWCVaCTw" width="100%" height="100%" style="position:absolute" frameBorder="0" class="giphy-embed" allowFullScreen></iframe></div><p><a href="https://giphy.com/gifs/atletica-atleticafeausp-fea-usp-1l9wPn8viAMWCVaCTw">via GIPHY</a></p> -->
      </div>
      <span class="new-title text-end d-flex align-items-center"> Keepr-oo</span>
    </router-link>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <li>
          <!-- <router-link :to="{ name: 'About' }" class="btn theme-btn lighten-30 selectable theme-font">
            About
          </router-link> -->
          <!-- <ModalWrapper id="id" v-if="user.isAuthenticated">s -->
          <button v-if="user.isAuthenticated" class="btn selectable theme-btn theme-font" data-bs-toggle="modal"
            data-bs-target="#create-keep">
            Create a Keep
            <CreateKeepForm />
          </button>
        </li>


      </ul>
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>
</template>

<script>
import Login from './Login.vue';
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import ModalWrapper from './ModalWrapper.vue';
import CreateKeepForm from './CreateKeepForm.vue';
import CreateVaultForm from './CreateVaultForm.vue';
export default {
  setup() {
    return {
      user: computed(() => AppState.user)

    }
  },
  components: { Login, CreateKeepForm }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

/* FIXME - change font family */
.new-title {
  color: var(--themeCharcoal);
  font-family: 'Courgette', cursive;
  font-size: 5dvh;
}

.theme-font {
  font-family: 'Courgette', cursive;
  font-size: 2.5dvh;
  ;
}

.logo {

  transform: rotateY(180deg);
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

.theme-btn:hover {
  background-color: var(--themeMauve);
}

nav {
  /* background-color: var(--themeWhite); */
  color: var(--theme);

}

.navbar-toggler-icon {
  background-color: var(--themeBlush);
}

/* @media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
} */
</style>
