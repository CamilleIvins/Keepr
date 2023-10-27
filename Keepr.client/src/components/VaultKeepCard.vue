<template>
    <div class="hover position-relative">
        <button @click="deleteKeep" v-if="vk.creatorId == account.id" class="mdi mdi- text-center delete-keep btn">Delete
            Keep</button>
        <img v-if="vk.id" :src="vk.img" class="keepCover" :alt="vk.name">
        <!-- </ModalWrapper> -->
        <div class="overlay-card text-light p-1">
            <section class="row">
                <p class="d-flex align-items-center col-8 ps-3 my-0 fs-md-3 fs-5 font-cour">{{ vk.name }}</p>
                <router-link class="col-4 d-flex justify-content-end align-items-end align-items-md-center"
                    :to="{ name: 'Profile', params: { profileId: vk.creatorId } }">
                    <img class="rounded-circle col-12 my-0 p-md-1 profile-pic" :src="vk.creator.picture"
                        :alt="vk.creator.name">
                </router-link>
            </section>

        </div>

    </div>
    <!-- <div class="component">
         <div class="component col-md-3 col-6">
        <div class="position-relative hover">
           <ModalWrapper id="keep-modal"> -->
    <!-- <img :src="vk.img" class="vkCover" :alt="vk.name" @click="openvkDetails()" data-bs-toggle="modal"
                data-bs-target="#vk-modal"> -->
    <!-- </ModalWrapper> 
            <div class="overlay-card text-light p-1">
                <section class="row">
                    <p class="d-flex align-items-center col-8 ps-3 my-0 fs-md-3 fs-5 font-cour">{{ vk.name }}</p>
                    <router-link class="col-4 d-flex justify-content-end align-items-end align-items-md-center"
                        :to="{ name: 'Profile', params: { profileId: vk.creatorId } }">

                        <img class="rounded-circle col-12 my-0 p-md-1 profile-pic" :src="vk.creator.picture"
                            :alt="vk.creator.name">
                    </router-link>
                </section>

            </div>
        </div>

    </div> -->
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { VaultKeep } from '../models/Keep.js';
import Pop from '../utils/Pop.js';
import { useRoute } from 'vue-router';
import { logger } from '../utils/Logger.js';
import { vaultKeepsService } from '../services/VaultKeepsService.js';

export default {
    props: { vk: { type: VaultKeep || Object, required: true }, },
    // propsB: { } },
    setup(props) {
        // const route = useRoute()
        return {

            async deleteKeep() {
                try {
                    const activeVK = props.vk.vaultKeepId
                    logger.log(activeVK)
                    const removeVK = await Pop.confirm("Are you sure you wish to remove this Keep from its Vault");
                    if (!removeVK) {
                        return;
                    }

                    await vaultKeepsService.deleteVK(activeVK);
                    AppState.activeKeep.kept--;
                    Pop.toast("Keep successfully removed!");
                }
                catch (error) {
                    Pop.error(error);
                }
            },
            // THIS WILL GO INTO MODAL
            // async deleteKeep(keepId) {
            //     try {
            //         const deletedKeep = await Pop.confirm("Are you sure you want to delete this Keep?")
            //         if (!deletedKeep) {
            //             return
            //         }
            //         await keepsService.deleteKeep(keepId)
            //     } catch (error) {
            //         Pop.error(error)
            //     }
            // }
            // async keepModal() {
            //     // trycatch these
            //     try {
            //         const keepId = route.params.keepId
            //         const modalView = await keepsService.createKeepModal(keepId)
            //         logger.log(modalView)
            //         Modal.getOrCreateInstance('#keepModal').show()
            //     } catch (error) {
            //         Pop.error(error)
            //     }
            // }
            account: computed(() => AppState.account),


        };
    },
    // components: { ModalWrapper }
};
</script>


<style lang="scss" scoped>
.keepCover {

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

.delete-keep {
    // position: relative;
    top: 160px;
    color: var(--themeRoo);
    // background-color: var(--themeWhite);
}

.delete-keep:hover {
    color: var(--themeRoo);
    // background-color: var(--themeRoo);
}

.profile-pic {
    // height: max-content;
    max-height: 50px;
    max-width: 50px;
}

.profile-pic:hover {
    // height: max-content;
    transform: scale(1.05);
    transition: 0.35s;
}

@media screen and (max-width: 768px) {
    .profile-pic {
        height: max-content;
        max-height: 50px;
    }
}
</style>


<!-- not working code || instacult-->
 <!-- export default {
    const props = defineProps{ { vk: { type: VaultKeep || Object, required: true} } }
    const canRemove = computed(() => AppState.account.id == AppState.activeVaultKeep?.creatorId)
     propsB: { } },
     setup(props) {
     const route = useRoute()
     return {
    
    async function deleteKeep() {
        try {
            await vaultKeepsService.deleteVK(props.vk.vaultKeepId)
             const removeVK = await Pop.confirm("Are you sure you wish to remove this Keep from its Vault");
             if (!removeVK) {
                 return;
             }
    
             await vaultKeepsService.deleteVK(props.vk.vaultKeepId);
             AppState.activeKeep.kept--;
            Pop.toast("Keep successfully removed!");
        }
        catch (error) {
            Pop.error(error);
        }
    }
     THIS WILL GO INTO MODAL
     async deleteKeep(keepId) {
         try {
             const deletedKeep = await Pop.confirm("Are you sure you want to delete this Keep?")
             if (!deletedKeep) {
                 return
             }
             await keepsService.deleteKeep(keepId)
         } catch (error) {
             Pop.error(error)
         }
     }
     async keepModal() {
          trycatch these
         try {
             const keepId = route.params.keepId
             const modalView = await keepsService.createKeepModal(keepId)
             logger.log(modalView)
             Modal.getOrCreateInstance('#keepModal').show()
         } catch (error) {
             Pop.error(error)
         }
     }
     account: computed(() => AppState.account),
    
    
    // };
    // },
    // components: { ModalWrapper }
    // }; -->