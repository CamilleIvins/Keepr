<template>
    <section class="container-fluid">

        <section>

        </section>
        <section>

        </section>
        <section>

        </section>

    </section>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Vault } from '../models/Vault.js';
export default {
    props: { vault: { type: Vault, required: true }, },
    setup() {
        return {}
    }
};
</script>


<style lang="scss" scoped></style>