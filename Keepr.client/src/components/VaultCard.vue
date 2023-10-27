<template>
    <div class="component container-fluid">
        <!-- <div class="component col-md-3 col-6"> -->
        <div v-if="vault.isPrivate == false" class="position-relative hover">
            <router-link class="col-4" :to="{ name: 'Vault Details', params: { vaultId: vault.id } }">
                <img :src="vault.img" class="vaultCover" :alt="vault.name">
                <div class="overlay-card text-light p-1">
                    <section class="row">
                        <p class="d-flex align-items-center col-8 ps-3 my-0 fs-md-3 fs-4 font-cour">{{ vault.name }}</p>
                        <router-link class="col-4 d-flex justify-content-end align-items-end align-items-md-center"
                            :to="{ name: 'Profile', params: { profileId: vault.creatorId } }">
                            <!-- TODO toggle this so that it is unlocked or locked on the VAULT DETAILS page -->
                            <img class="rounded-circle col-12 my-0 p-md-1 profile-pic" :src="vault.creator.picture"
                                :alt="vault.creator.name">
                        </router-link>

                    </section>

                </div>
            </router-link>
        </div>
        <div v-else class="position-relative hover">
            <div v-if="vault.creatorId == account.id">
                <router-link class="col-4" :to="{ name: 'Vault Details', params: { vaultId: vault.id } }">
                    <img :src="vault.img" class="vaultCover">
                    <div class="overlay-card text-light p-1">
                        <section class="row">
                            <p class="d-flex align-items-center col-8 ps-3 my-0 fs-md-3 fs-4 font-cour">{{ vault.name }}</p>
                            <span class="d-flex align-items-end p-1 col-4"><i class="mdi mdi-lock-outline text-light"><span
                                        class="toggle-text">Private</span></i></span>
                            <!-- <img class="rounded-circle col-12 my-0 p-md-1 profile-pic" :src="vault.creator.picture"> -->

                        </section>
                    </div>
                </router-link>
            </div>
            <div v-else>
                <router-link class="col-4" :to="{ name: 'Home', path: (`/`) }">
                    <img :src="vault.img" class="vaultCover">
                    <div class="overlay-card text-light p-1">
                        <section class="row">
                            <p class="d-flex align-items-center col-8 ps-3 my-0 fs-md-3 fs-4 font-cour">{{ vault.name }}</p>
                            <span class="d-flex align-items-end p-1 col-4"><i class="mdi mdi-lock-outline text-light"><span
                                        class="toggle-text">Private</span></i></span>
                            <!-- <img class="rounded-circle col-12 my-0 p-md-1 profile-pic" :src="vault.creator.picture"> -->

                        </section>

                    </div>
                </router-link>
            </div>

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
    // add aspect ratio because new vault's do not conform
    min-height: 15dvh;
    border-radius: 15px;
    width: 100%;
    aspect-ratio: 5/4;
    object-fit: cover;
    object-position: center;
    box-shadow: 0 -0.5px 8px -0.5px #2928289a,
        0 -1px 8px -2px #29282894;
}

.hover:hover {
    transform: scale(1.02);
    transition: 0.35s;
}

.font-cour {
    color: var(--themeWhite);
    filter: drop-shadow(0.5px 1px 1px var(--themeCharcoal));
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

// FIXME - fix this issue w/ display inline vs none
.toggle-text {
    display: inline;
}

@media (max-width: 768px) {
    .profile-pic {
        height: max-content;
        max-height: 50px;
    }

    .toggle-text {
        display: none;
        ;
    }
}
</style>