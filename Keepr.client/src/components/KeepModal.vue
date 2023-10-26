<template>
    <div class="modal fade" id="keep-modal" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false" role="dialog"
        aria-labelledby="modalTitleId" aria-hidden="true">
        <!-- modal SIZE in listed in the below classes -->
        <div class="modal-dialog modal-dialog-scrollable modal-dialog-centered modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalTitleId"></h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <!-- NOTE the whole section has a v-if meaning it will only render IF there is an active movie -->
                        <!-- NOTE - need this top portion to be a ROW for desktop display to be side by side -->
                        <div class="component row align-items-center" v-if="keep.id">

                            <!-- LEFT to mobile TOP -->


                            <img :src="keep.img" alt="" class="col-md-6 col-12 mx-0 my-0">


                            <!-- RIGHT to mobile BOTTOM -->
                            <section class="col-md-6 col-12 mx-0 px-0">
                                <!-- viewcount, keep count centered -->
                                <section class="row justify-content-around mx-0 px-0">
                                    <div class="col-6 text-center">
                                        {{ keep.views }}<span class="px-2 mdi mdi-eye-outline"></span>
                                        <span class="">||
                                        </span>
                                        {{ keep.kept }}<span class="px-2 mdi mdi-alpha-k-circle-outline"></span>
                                    </div>
                                </section>
                                <!-- title, description, centered -->
                                <section class="row justify-content-center mx-0 px-0 my-md-5">
                                    <div class="fs-1">{{ keep.name }}</div>
                                    <p>{{ keep.description }}</p>
                                </section>
                                <!-- vault category - account list - vis only to logged in, save/remove if logged in, profile of creator -->
                                <!-- all centred -->
                                <section class="row align-items-center justify-content-around mt-md-5 mx-0 px-0">
                                    <!-- dropdown, v-if logged in & kept  -->
                                    <div v-if="account.id != null" class="col-3">
                                        <!-- OR update the keep -->
                                        <form @submit.prevent="updateVK">
                                            <label class="mini">Choose a Vault</label>
                                            <select v-model="editable.vaultId" name="myVaults" id="myVaults"
                                                class="form-control">
                                                <option value="" disabled selected>Choose Vault</option>
                                                <option v-for="vault in myVaults" :key="'select' + vault.id"
                                                    :value="vault.id">{{ vault.name }}</option>
                                            </select>
                                        </form>
                                    </div>
                                    <!-- shows if logged in -->
                                    <!-- save/remove - which will trigger above to show -->
                                    <div v-if="account.id != null" class="col-3">

                                        <!-- <div v-if="account.id==keep.creatorId && vaultKeep.keepId == null"> -->
                                        <div v-if="vaultKeep.keepId == null">
                                            <!-- save -->
                                            <button @click="createVK" class="save-keep btn">
                                                Save
                                            </button>
                                        </div>

                                        <div v-else>
                                            <!-- delete -->
                                            <button @click="removeVK" class="btn remove-keep">
                                                Remove
                                                <!--data-bs-toggle="collapse" data-bs-target="#VaultList">
                                                <div class="collapse" id="VaultList"> -->

                                            </button>

                                        </div>
                                    </div>
                                    <!-- shows always -->
                                    <!-- creator pic -->
                                    <div class="text-end col-6 dip">
                                        <img class="profile-pic rounded-circle" :src="keep.creator.picture"
                                            :title="keep.creator.name" data-toggle="name" data-placement="bottom"
                                            alt="Keep Creator"> {{ keep.creator.name.slice(0,
                                                keep.creator.name.indexOf('@')) }}
                                    </div>
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
import { computed, reactive, onMounted, ref } from 'vue';
import Pop from '../utils/Pop.js';
import { keepsService } from '../services/KeepsService.js';
import { vaultKeepsService } from '../services/VaultKeepsService.js';


export default {
    // props: { keep: { type: Keep || Object, required: true }, },

    setup() {

        const editable = ref({})

        return {
            // TODO - VK service
            // FIXME - VK service
            async createVK(keepId) {
                try {
                    const vkStatus = editable.value
                    vkStatus.keepId = keepId
                    vaultKeepsService.createVK(vkStatus)
                } catch (error) {
                    Pop.error(error)
                }
            },

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
            },

            keep: computed(() => AppState.activeKeep),
            // profile: computed(() => AppState.profile),
            editable,
            account: computed(() => AppState.account),
            myVaults: computed(() => AppState.myVaults),
            vaultKeep: computed(() => AppState.vaultkeep),
        }
    }
};
</script>


<style lang="scss" scoped>
.profile-pic {
    max-height: 50px;
}

.profile-pic:hover {
    box-shadow: 0 2px 8px var(--themeFadedAmethyst);
    transition: 0.35s;
}

.mini {
    font-size: 1.5dvh;
    color: var(--themeFadedAmethyst);
}

.save-keep:hover {
    background-color: var(--themeFadedAmethyst);
    color: var(--themeCream);
}

.save-keep {
    background-color: var(--themeMauve);
    color: var(--themeFadedAmethyst);
}

.remove-keep:hover {
    background-color: var(--themeFadedAmethyst);
    color: var(--themeRoo);
}

.remove-keep {
    background-color: var(--themeWhite);
    color: var(--themeRoo);
}

// .dip {
//     position: absolute;
//     right: 2%;
//     bottom: 3%;
// }
</style>