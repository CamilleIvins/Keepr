<template>
    <div class="component">
        <!-- <div class="component col-md-3 col-6"> -->
        <div class="position-relative hover">
            <!-- <ModalWrapper id="keep-modal"> -->
            <img :src="keep.img" class="keepCover" :alt="keep.name" @click="openKeepDetails()" data-bs-toggle="modal"
                data-bs-target="#keep-modal">
            <!-- </ModalWrapper> -->
            <div class="overlay-card text-light p-1">
                <section class="row">
                    <p class="d-flex align-items-center col-8 ps-3 my-0 fs-md-3 fs-5 font-cour">{{ keep.name }}</p>
                    <router-link class="col-4 d-flex justify-content-end align-items-end align-items-md-center"
                        :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">

                        <img class="rounded-circle col-12 my-0 p-md-1 profile-pic" :title="keep.creator.name"
                            data-toggle="name" data-placement="bottom" :src="keep.creator.picture" :alt="keep.creator.name">
                    </router-link>
                </section>

            </div>
        </div>

    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Keep } from '../models/Keep.js';
import { Profile } from '../models/Account.js';
import { keepsService } from '../services/KeepsService.js';
import Pop from '../utils/Pop.js';
import { useRoute } from 'vue-router';
import { logger } from '../utils/Logger.js';
import { Modal } from 'bootstrap';
import ModalWrapper from './ModalWrapper.vue';
export default {
    props: { keep: { type: Keep || Object, required: true }, },
    // propsB: { } },
    setup(props) {
        // const route = useRoute()
        return {
            async openKeepDetails() {
                try {
                    const keepId = props.keep.id;
                    logger.log(keepId);
                    await keepsService.getKeepById(keepId);

                    // const modal = Modal.getOrCreateInstance('#keep-modal')
                    // modal.show()
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