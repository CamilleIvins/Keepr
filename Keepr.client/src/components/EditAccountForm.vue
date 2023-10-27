<template>
    <div class="modal fade" id="edit-account" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false" role="dialog"
        aria-labelledby="modalTitleId" aria-hidden="true">
        <!-- modal SIZE in listed in the below classes -->
        <div class="modal-dialog modal-dialog-scrollable modal-dialog-centered modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="" id="accountUpdate"> Something new, something you</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <!-- <button @click.prevent="createVault">
    Place this on the navbar, Vaults will be on account
</button> -->
                        <!-- TODO - HIDE in function 'getOCreateInstance' part on submit -->
                        <form @submit.prevent="editAccount" class="row">
                            <!-- Do NOT make all these required -->
                            <div class="mb-1 col-12">
                                <label for="name">Name</label>
                                <input v-model="accountData.name" required type="text" minlength="3" maxlength="50"
                                    class="form-control" id="name" placeholder="Name">
                            </div>

                            <div class="mb-1 col-12">
                                <label for="email">E-mail</label>
                                <input v-model="accountData.email" type="email" maxlength="50" class="form-control"
                                    id="email" placeholder="E-mail">
                            </div>

                            <div class="mb-1 col-12">
                                <label for="picture">Picture URL</label>
                                <input v-model="accountData.picture" type="text" maxlength="250" class="form-control"
                                    id="picture" placeholder="Picture">
                            </div>
                            <div class="mb-1 col-12 d-flex justify-content-center">
                                <img :src="accountData.picture" alt="" class="preview-img rounded-circle"
                                    style="max-height: 25;">
                            </div>
                            <hr>
                            <!-- SAVE FOR A MORE AMBITIOUS DAY -->
                            <!-- <div class="row">

                                <i class="mdi mdi-linkedin fs-1 col-1 text-end"></i>
                                <span class="col-md-4 col-11">
                                    <div class="form-floating mb-2 ">
                                        <input v-model="accountData.linkedin" type="url" maxlength="100"
                                            class="form-control" id="linkedin" placeholder="linkedin">
                                        <label for="linkedin">Share Profile</label>
                                    </div>
                                </span>
                                <i class="mdi mdi-github fs-1 col-1 text-end"></i>
                                <span class="col-md-4 col-11">
                                    <div class="form-floating mb-2 ">
                                        <input v-model="accountData.github" type="url" maxlength="500" class="form-control"
                                            id="github" placeholder="github">
                                        <label for="github">Share Profile</label>
                                    </div>
                                </span>
                            </div> -->

                            <!-- <div class="row">

                                <i class="mdi mdi-email fs-1 col-1 text-end"></i>
                                <span class="col-md-4 col-11">
                                    <div class="form-floating mb-2 ">
                                        <input v-model="accountData.email" type="email" maxlength="500" class="form-control"
                                            id="email" placeholder="email">
                                        <label for="email">Share Contact Info</label>
                                    </div>
                                </span>

                            </div>

                            <div class="row justify-content-center mb-2">

                                <textarea v-model="accountData.bio" name="bio" id="bio" class="col-md-10 ms-md-4 ms-0 col-12"
                                    cols="30" rows="10" maxlength="1000" placeholder="Bio of champions"></textarea>
                            </div>
                            <div class="row justify-content-center">
                                <button type="submit" class="rounded my-2 col-6 elevation-4"
                                    id="update-profile">Share!</button>

                            </div> -->

                            <div class="mb-1 col-12">
                                <label class="text-dark" for="account-cover">Cover Image</label>
                                <input v-model="accountData.CoverImg" type="url" id="account-cover" class="form-control"
                                    minlength="5" maxlength="250" placeholder="Account Cover URL" required>
                            </div>
                            <div class="mb-1 col-12 d-flex justify-content-center">
                                <img :src="accountData.CoverImg" alt="" class="img-fluid preview-img" required>
                            </div>
                            <!-- check character cap in SQL/Dapper ✅-->

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
import Pop from '../utils/Pop.js';
import { accountService } from '../services/AccountService.js';
export default {
    setup() {
        const accountData = ref({})
        const router = useRouter()
        return {
            accountData,
            async editAccount() {
                try {
                    await accountService.editAccount(accountData.value)
                    Pop.success("Your account has been updated")
                    router.push({ name: 'Account' })
                } catch (error) {
                    Pop.error(error)
                }
            }
        }
    }
};
</script>


<style lang="scss" scoped>
.preview-img {

    width: 50%;
    height: auto
}
</style>