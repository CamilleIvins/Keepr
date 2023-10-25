<template>
    <div class="modal fade" id="keep-modal" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false" role="dialog"
        aria-labelledby="modalTitleId" aria-hidden="true">
        <div class="modal-dialog modal-dialog-scrollable modal-dialog-centered modal-xl" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalTitleId">Modal title</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <!-- NOTE the whole section has a v-if meaning it will only render IF there is an active movie -->
                        <div class="component" v-if="keep.id">

                            <!-- LEFT to mobile TOP -->
                            <section class="col-md-6 col-12">
                                <img :src="keep.img" alt="">
                            </section>

                            <!-- RIGHT to mobile BOTTOM -->
                            <section class="col-md-6 col-12">
                                <!-- viewcount, keep count centered -->
                                <section class="row justify-content-center">
                                    {{ keep.views.length }} || {{ keep.kept.length }}
                                </section>
                                <!-- title, description, centered -->
                                <section class="text-center">
                                    <div>{{ keep.name }}</div>
                                    <p>{{ keep.description }}</p>
                                </section>
                                <!-- vault category - account list - vis only to logged in, save/remove if logged in, profile of creator -->
                                <!-- all centred -->
                                <section>
                                    <!-- dropdown, v-if logged in & kept  -->
                                    <span>

                                    </span>
                                    <!-- shows if logged in -->
                                    <!-- save/remove - which will trigger above to show -->
                                    <span></span>
                                    <!-- shows always -->
                                    <!-- creator pic -->
                                    <span>
                                        <img class="profile-pic rounded-circle" :src="keep.creator.picture"
                                            :title="keep.creator.name" alt="Keep Creator">
                                    </span>
                                </section>
                            </section>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Keep } from '../models/Keep.js';

export default {
    // props: { keep: { type: Keep || Object, required: true }, },

    setup() {
        return {
            keep: computed(() => AppState.activeKeep),
            profile: computed(() => AppState.profile),
            account: computed(() => AppState.account)
        }
    }
};
</script>


<style lang="scss" scoped></style>