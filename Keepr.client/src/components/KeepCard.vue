<template>
    <div class="component" @click="keepModal()">
        <!-- <div class="component col-md-3 col-6"> -->
        <div class="position-relative">
            <img :src="keep.img" class="keepCover">
            <div class="overlay-card text-light p-1">
                <section class="row">
                    <p class="d-flex align-items-center col-8 ps-3 my-0 fw-bold">{{ keep.name }}</p>
                    <router-link class="col-4" :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">

                        <img class="rounded-circle col-12 my-0 pe-3 pb-2 profile-pic" :src="keep.creator.picture">
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
export default {
    props: { keep: { type: Keep || Object, required: true }, profile: { type: Profile || Object, required: true } },
    // propsB: { } },

    setup() {
        const route = useRoute()

        return {
            async keepModal() {
                // trycatch these
                try {
                    const keepId = route.params.keepId
                    const modalView = await keepsService.createKeepModal(keepId)
                    logger.log(modalView)
                    Modal.getOrCreateInstance('#keepModal').show()
                } catch (error) {
                    Pop.error(error)
                }
            }
        }
    }
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


@media screen and (max-width: 768px) {
    .profile-pic {
        height: max-content;
    }
}
</style>