<template>
    <div class="container-fluid">
        <!-- <button @click.prevent="createVault">
    Place this on the account page, keep one can be on navbar
</button> -->

        <form @submit.prevent="createVault" class="row">
            Add a Vault!
            <div class="mb-1 col-6">
                <label class="text-dark" for="vault-name">Vault Title</label>
                <input v-model="vaultData.name" id="vault-name" class="form-control" type="text" minlength="5"
                    maxlength="55" placeholder="Vault Title" required>
            </div>

            <div class="mb-1 col-12">
                <label class="text-dark" for="vault-cover">Cover Image</label>
                <input v-model="vaultData.img" type="url" id="vault-cover" class="form-control" minlength="5"
                    maxlength="250" placeholder="Vault Cover URL">
            </div>
            <div class="mb-1 col-12">
                <img :src="vaultData.img" alt="" class="img-fluid preview-img">
            </div>
            <!-- place ⬇️ on vault details page -->
            <!-- <div class="mb-1 col-12">
            <label for="vault-description" class="text-dark">vault Description</label>
            <textarea v-model="vaultData.description" type="number" id="vault-description" class="form-control" maxlength="1000" cols="30" rows="10" placeholder="Vault Description"></textarea>

        </div> -->
            <section class="row justify-content-end">
                <div class="form-check col-6">
                    <input class="form-check-input" type="checkbox" value="" id="vault-privacy">
                    <label class="form-check-label" for="flexCheckDefault">
                        Make this vault private?
                    </label>
                </div>

                <div class="col-6 text-end">
                    <button class="btn text-dark">SUBMIT</button>
                </div>
            </section>


        </form>

    </div>
</template>


<script>
import { useRouter } from 'vue-router';
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { vaultsService } from '../services/VaultsService.js'
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { Modal } from 'bootstrap';
export default {
    setup() {
        const vaultData = ref({})
        const router = useRouter()
        function resetForm() {
            vaultData.value = { category: '' }
        }
        onMounted(() => {
            resetForm()
        })

        return {
            vaultData,
            async createVault() {
                // remember to try-catch these
                try {
                    logger.log("create button pushed")
                    let newVault = await vaultsService.createVault(vaultData.value)
                    Pop.toast('vault created', 'success')
                    resetForm()
                    // ID is found in NAVBAR
                    Modal.getOrCreateInstance('#create-vault').hide()
                    router.push({ name: 'Vault Details', params: { vaultId: newVault.id } }) //<--did not like params, still created

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