<template>
    <div class="component container-fluid">
        <!-- <div class="component col-md-3 col-6"> -->
        <div v-if="vault.isPrivate == false" class="position-relative hover">
            <router-link class="col-4" :to="{ name: 'Vault Details', params: { vaultId: vault.id } }">
                <img :src="vault.img" class="vaultCover">
                <div class="overlay-card text-light p-1">
                    <section class="row">
                        <p class="d-flex align-items-center col-8 ps-3 my-0 fw-bold">{{ vault.name }}</p>
                        <router-link class="col-4 d-flex justify-content-end align-items-end align-items-md-center"
                            :to="{ name: 'Profile', params: { profileId: vault.creatorId } }">
                            <!-- TODO toggle this so that it is unlocked or locked on the VAULT DETAILS page -->
                            <img class="rounded-circle col-12 my-0 p-md-1 profile-pic" :src="vault.creator.picture">
                        </router-link>

                    </section>

                </div>
            </router-link>
        </div>
        <div v-else class="position-relative hover">
            <router-link class="col-4" :to="{ name: 'Vault Details', params: { vaultId: vault.id } }">
                <img :src="vault.img" class="vaultCover">
                <div class="overlay-card text-light p-1">
                    <section class="row">
                        <p class="d-flex align-items-center col-8 ps-3 my-0 fw-bold">{{ vault.name }}</p>
                        <i class="mdi mdi-lock-outline text-light">Private</i>
                        <!-- <img class="rounded-circle col-12 my-0 p-md-1 profile-pic" :src="vault.creator.picture"> -->

                    </section>

                </div>
            </router-link>
        </div>

    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Vault } from '../models/Vault.js';
// import { Profile } from '../models/Account.js';
import { vaultsService } from '../services/VaultsService.js';
import Pop from '../utils/Pop.js';
import { useRoute } from 'vue-router';
import { logger } from '../utils/Logger.js';
import { Modal } from 'bootstrap';
export default {
    props: { vault: { type: Vault || Object, required: true }, },
    // propsB: { } },

    setup() {
        const route = useRoute()

        return {
            async vaultModal() {
                // trycatch these
                try {
                    const vaultId = route.params.vaultId
                    const modalView = await vaultsService.createVaultModal(vaultId)
                    logger.log(modalView)
                    Modal.getOrCreateInstance('#vaultModal').show()
                } catch (error) {
                    Pop.error(error)
                }
            },

            account: computed(() => AppState.account),
            profile: computed(() => AppState.activeProfile),

        }
    }
};
</script>


<style lang="scss" scoped>
.vaultCover {

    min-height: 15dvh;
    border-radius: 15px;
    width: 100%;
    object-fit: cover;
    object-position: center;
    box-shadow: 0 -0.5px 8px -0.5px #2928289a,
        0 -1px 8px -2px #29282894;
}

.hover:hover {
    transform: scale(1.02);
    transition: 0.35s;
}

.overlay-card {
    background: linear-gradient(to top,
            hsl(0, 0%, 0%) 0%,
            hsla(0, 0%, 0%, 0.738) 19%,
            hsla(0, 0%, 0%, 0.541) 34%,
            hsla(0, 0%, 0%, 0.382) 47%,
            hsla(0, 0%, 0%, 0.278) 56.5%,
            hsla(0, 0%, 0%, 0.194) 65%,
            hsla(0, 0%, 0%, 0.126) 73%,
            hsla(0, 0%, 0%, 0.075) 80.2%,
            hsla(0, 0%, 0%, 0.042) 86.1%,
            hsla(0, 0%, 0%, 0.021) 91%,
            hsla(0, 0%, 0%, 0.008) 95.2%,
            hsla(0, 0%, 0%, 0.002) 98.2%,
            hsla(0, 0%, 0%, 0) 100%);
    position: absolute;
    bottom: 0;
    width: 100%;
    border-bottom-left-radius: 15px;
    border-bottom-right-radius: 15px;

}


.profile-pic {
    // height: max-content;
    max-height: 50px;
    max-width: 50px;
}

@media screen and (max-width: 768px) {
    .profile-pic {
        height: max-content;
        max-height: 50px;
    }
}
</style>