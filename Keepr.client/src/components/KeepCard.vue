<template>
    <div class="component">
        <!-- <div class="component col-md-3 col-6"> -->
        <div class="position-relative">
            <img :src="keep.img" class="keepCover">
            <div class="overlay-card text-light p-1">
                <section class="row">
                    <p class="d-flex align-items-center col-8 ps-3 my-0 fw-bold">{{ keep.name }}</p>
                    <img class="rounded-circle col-4 my-0 pe-3 pb-2 profile-pic" :src="keep.creator.picture">
                </section>

            </div>
        </div>

    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Keep } from '../models/Keep.js';
export default {
    props: { keep: { type: Keep || Object, required: true } },

    setup() {
        return {}
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