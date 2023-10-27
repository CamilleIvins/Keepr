<template>
    <div class="modal fade" id="keep-modal" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false" role="dialog"
        aria-labelledby="modalTitleId" aria-hidden="true">
        <!-- modal SIZE in listed in the below classes -->
        <div class="modal-dialog modal-dialog-scrollable modal-dialog-centered modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <i v-if="keep.creatorId == account.id" @click="deleteKeep(keepId)"
                        class="mdi mdi-cancel text-end delete-keep btn">Delete Keep</i>

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
                                    <div v-if="account.id != null" class="col-3 px-0 d-flex">
                                        <form @submit.prevent="createVK">
                                            <!-- OR update the keep -->
                                            <label class="mini">Choose a Vault</label>
                                            <select v-model="vaultKeepData.vaultId" name="myVaults" id="myVaults"
                                                class="form-control">
                                                <option value="" disabled selected>Choose Vault</option>
                                                <option v-for="vault in myVaults" :key="'select' + vault.id"
                                                    :value="vault.id">{{ vault.name }}</option>
                                            </select>
                                            <!-- shows if logged in -->
                                            <!-- save/remove - which will trigger above to show -->
                                            <div v-if="account.id != null" class="d-flex col-3 mt-1 text-end">

                                                <!-- <div v-if="account.id==keep.creatorId && vaultKeep.keepId == null"> -->
                                                <div>
                                                    <!-- <div v-if="vaultKeepData.vaultId != vaultKeepData.vaultId"> -->
                                                    <!-- save -->
                                                    <button class="save-keep btn">
                                                        Save
                                                    </button>
                                                </div>

                                                <!--  <div v-else>
                                                    delete -->
                                                <!-- <button class="btn remove-keep" disabled>
                                                        Save
                                                        data-bs-toggle="collapse" data-bs-target="#VaultList">
                                                    <div class="collapse" id="VaultList"> 

                                                    </button> 

                                                </div>-->
                                            </div>
                                        </form>
                                    </div>
                                    <!-- shows always -->
                                    <!-- creator pic -->
                                    <div class="text-end col-6">
                                        <router-link class=" theme-font"
                                            :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">

                                            <img class="profile-pic rounded-circle" :src="keep.creator.picture"
                                                :title="keep.creator.name" data-toggle="name" data-placement="bottom"
                                                alt="Keep Creator"> {{ keep.creator.name.slice(0,
                                                    keep.creator.name.indexOf('@')) }}
                                        </router-link>
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
import { accountService } from '../services/AccountService.js';
import { logger } from '../utils/Logger.js';
import { useRoute } from 'vue-router';


export default {
    // props: { keep: { type: Keep || Object, required: true }, },

    setup() {
        const route = useRoute()

        onMounted(() => {
            // getMyVaults()
        })

        const vaultKeepData = ref({})
        // async function getMyVaults() {
        //     try {
        //         await accountService.getMyVaults()
        //     } catch (error) {
        //         Pop.error(error)
        //     }
        // }

        return {
            // TODO - VK service
            // FIXME - VK service
            async createVK() {
                try {
                    // debugger
                    vaultKeepData.value.keepId = AppState.activeKeep.id
                    logger.log(vaultKeepData.value)
                    await vaultKeepsService.createVK(vaultKeepData.value)
                    // AppState.activeKeep.kept++
                } catch (error) {
                    Pop.error(error)
                }
                // try {
                //     const vkStatus = editable.value
                //     vkStatus.keepId = keepId
                //     vaultKeepsService.createVK(vkStatus)
                // } catch (error) {
                //     Pop.error(error)
                // }
            },
            // Move to Vault Details Page
            // async removeVK() {
            //     try {
            //         const removeKeep = await Pop.confirm("Are you sure you wish to remove this Keep from its Vault?")
            //         if (!removeKeep) {
            //             return
            //         }
            //         debugger

            //         await vaultKeepsService.deleteVK(vaultKeepData)
            //     } catch (error) {
            //         Pop.error(error)
            //     }
            // },

            async deleteKeep(keepId) {
                try {
                    const keepId = AppState.activeKeep.id
                    const deletedKeep = await Pop.confirm("Are you sure you want to delete this Keep?")
                    if (!deletedKeep) {
                        return
                    }
                    await keepsService.deleteKeep(keepId)
                    return AppState.myKeeps;
                } catch (error) {
                    Pop.error(error)
                }
            },

            keep: computed(() => AppState.activeKeep),
            // profile: computed(() => AppState.profile),
            vaultKeepData,
            account: computed(() => AppState.account),
            myVaults: computed(() => AppState.myVaults),
            vaultKeep: computed(() => AppState.activeVaultKeep),
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

.delete-keep {
    color: var(--themeRoo);
    background-color: var(--themeWhite);
}

.delete-keep:hover {
    color: var(--themeWhite);
    background-color: var(--themeRoo);
}

.mini {
    font-size: 1.5dvh;
    color: var(--themeFadedAmethyst);
}

.theme-font {
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