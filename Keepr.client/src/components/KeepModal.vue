<template>
    <div class="component">

        <!-- LEFT to mobile TOP -->
        <section>

        </section>

        <!-- RIGHT to mobile BOTTOM -->
        <section>

        </section>

    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Keep } from '../models/Keep.js';

export default {
    props: { keep: { type: Keep || Object, required: true }, },

    setup() {
        return {}
    }
};
</script>


<style lang="scss" scoped></style>