<template>
    <div class="modal fade" id="create-keep" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false" role="dialog"
        aria-labelledby="modalTitleId" aria-hidden="true">
        <!-- modal SIZE in listed in the below classes -->
        <div class="modal-dialog modal-dialog-scrollable modal-dialog-centered modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalTitleId"> Add a Keep!</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <!-- <button @click.prevent="createVault">
    Place this on the navbar, Vaults will be on account
</button> -->
                        <!-- TODO - HIDE in function 'getOCreateInstance' part on submit -->
                        <form @submit.prevent="createKeep" class="row">
                            <div class="mb-1 col-6">
                                <label class="text-dark" for="keep-name">Keep Title</label>
                                <input v-model="keepData.name" id="keep-name" class="form-control" type="text" minlength="3"
                                    maxlength="55" placeholder="Keep Title" required>
                            </div>

                            <div class="mb-1 col-12">
                                <label class="text-dark" for="keep-cover">Cover Image</label>
                                <input v-model="keepData.img" type="url" id="keep-cover" class="form-control" minlength="5"
                                    maxlength="250" placeholder="Keep Cover URL" required>
                            </div>
                            <div class="mb-1 col-12">
                                <img :src="keepData.img" alt="" class="img-fluid preview-img" required>
                            </div>
                            <!-- check character cap in SQL/Dapper ✅-->
                            <div class="mb-1 col-12">
                                <label for="keep-description" class="text-dark">Keep Description</label>
                                <textarea v-model="keepData.description" type="number" id="keep-description"
                                    class="form-control" maxlength="1000" cols="30" rows="10" placeholder="Keep Description"
                                    required></textarea>

                            </div>
                            <section class="row justify-content-end">


                                <div class="col-6 text-end">
                                    <button class="btn text-dark">SUBMIT</button>
                                </div>
                            </section>


                        </form>

                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { useRouter } from 'vue-router';
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { keepsService } from '../services/KeepsService.js'
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { Modal } from 'bootstrap';
export default {
    setup() {
        const keepData = ref({})
        const router = useRouter()
        function resetForm() {
            keepData.value = { category: `` }
        }
        onMounted(() => {
            resetForm()
        })

        return {
            keepData,
            async createKeep() {
                // remember to try-catch these
                try {
                    logger.log("create button pushed")
                    let newKeep = keepData.value
                    await keepsService.createKeep(newKeep)
                    Pop.toast('Keep Created', `This one's a Keepr`)
                    resetForm()
                    // ID is found in NAVBAR
                    // Modal.getOrCreateInstance('#create-keep').hide()
                    // NOTE - This might not need a push...
                    router.push({ name: 'Home' }) //<--did not like params, still created

                } catch (error) {
                    Pop.error(error)
                }

            }
        }
    }
};
</script>


<style lang="scss" scoped>
.preview-image {
    max-height: 15vh;
    object-fit: contain;
}
</style>

<!-- https://images.unsplash.com/photo-1655910836658-f4977367cfc6?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1927&q=80
 Join us at the Caramel Colosseum for a captivating weekend of caramel-cored combat!-->