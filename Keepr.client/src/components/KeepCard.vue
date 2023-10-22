<template>
    <div class="component">
        <div class="keepCover position relative">
            <img :src="keep.img" alt="">
            <div class="overlay-card text-light p-1">
                <p class="col-8">{{ keep.name }}</p>
                <p class="d-flex col-4">{{ keep.creator.picture }}</p>
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
            hsla(0, 0%, 0%, 0) 100%)
}
</style>