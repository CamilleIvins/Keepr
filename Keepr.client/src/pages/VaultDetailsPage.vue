<template>
    <section class="d-flex flex-column col-9 container-fluid mb-5" v-if="vault.id">

        <section class="row justify-content-center">
            <div class="col-md-8 col-12">
                <img :src="vault.img" class="vault-cover" alt="">
                <div class="text-center fs-1 img-title font-dancing">
                    {{ vault.name }}
                </div>
            </div>
            <div class="text-center mt-5">
                <!-- <p>{{ keeps.length }}Keeps</p> -->
            </div>
        </section>
        <div class="mt-5">
        </div>
        <div class="mt-5">
        </div>
        <!-- vault keeps -->
        <section class="row justify-content-center mt-5">
            <div v-if="vault.creatorId == account.id" class="col-md-6 col-12 text-center">
                <button @click="deleteVault" class="btn delete-vault">
                    Delete Vault
                </button>
            </div>
        </section>
        <section class="">
            hello
            <div v-for="vk in vaultKeeps" :key="vk.id">
                {{ vaultKeeps }} hello
                <img :src="vk.img" alt="">
            </div>

        </section>
        <section>

        </section>

    </section>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, watchEffect } from 'vue';
import { Vault } from '../models/Vault.js';
import { useRoute } from 'vue-router';
import { vaultsService } from '../services/VaultsService.js';
import { vaultKeepsService } from '../services/VaultKeepsService.js';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { router } from '../router.js';
export default {
    // props: { vault: { type: Vault, required: true }, },
    setup() {
        const route = useRoute()

        watchEffect(() => {
            getVaultById()
            getkeepsByVault()
        })

        async function getVaultById() {
            try {
                const vaultId = route.params.vaultId
                logger.log(vaultId)
                await vaultsService.getVaultById(vaultId)
            } catch (error) {
                Pop.error(error)
            }
        }

        async function getkeepsByVault() {
            try {
                const vaultId = route.params.vaultId
                await vaultKeepsService.getVaultKeeps(vaultId)
            } catch (error) {
                Pop.error(error)
            }
        }

        return {
            async deleteVK(vaultKeepId) {
                try {
                    const removeVK = await Pop.confirm("Are you sure you wish to remove this Keep from its Vault")
                    if (!removeVK) { return }
                    await vaultKeepsService.deleteVK(vaultKeepId)
                    Pop.toast("Keep successfully removed!")
                } catch (error) {
                    Pop.error(error)
                }

            },
            async deleteVault() {
                try {
                    const vaultId = AppState.activeVault.id
                    const removeVault = await Pop.confirm("Are you sure you wish to delete this Vault?", "All the Keeps within it will also be lost.")
                    if (!removeVault) { return }
                    await vaultsService.deleteVault(vaultId)
                    router.push({ name: 'Account' })
                    return AppState.myVaults
                } catch (error) {
                    Pop.error(error)
                }
            },
            account: computed(() => AppState.account),
            vault: computed(() => AppState.activeVault),
            vaultKeeps: computed(() => AppState.vaultkeeps.find(vk => vk.vaultId == AppState.activeVault.id)),

        }
    }
};
</script>


<style lang="scss" scoped>
.img-title {
    position: relative;
    text-align: center;
    bottom: -25%;
    filter: drop-shadow(3px 8px 7px #292828);
    color: aliceblue;
    font-size: xxx-large;
    text-shadow: .5px .5px 1px black;

}

.masonry-layout {
    $gap: 1.25em;
    columns: 4;
    column-gap: $gap;

    div {
        // max-height: 30dvh;
        // min-height: 15dvh;
        width: 100%;
        margin-bottom: $gap;
    }
}

.vault-cover {
    position: absolute;
    height: 35dvh;
    width: 100%;
    object-fit: cover;
    object-position: center;
    right: 0;
    /* elevation-2 settings */
    box-shadow: 0 3px 3px -1px rgba(205, 205, 205, 0.2),
        0 5px 6px 0 rgba(205, 205, 205, 0.14),
        0 1px 8px 0 rgba(205, 205, 205, 0.12);
}

@media screen and (max-width: 768px) {
    .masonry-layout {
        $gap: 1.25em;
        columns: 2;
        column-gap: $gap;

        div {
            // max-height: 30dvh;
            // min-height: 15dvh;
            width: 100%;
            margin-bottom: $gap;
        }
    }

    .vault-cover {
        position: absolute;
        height: 35vh;
        width: 100dvw;
        object-fit: cover;
        object-position: center;
        right: 0;
        /* elevation-2 settings */
        box-shadow: 0 3px 3px -1px rgba(205, 205, 205, 0.2),
            0 5px 6px 0 rgba(205, 205, 205, 0.14),
            0 1px 8px 0 rgba(205, 205, 205, 0.12);
    }

    .delete-vault {
        background-color: var(--themeMauve);
        color: var(--themeRoo);
    }

    .delete-vault:hover {
        background-color: var(--themeFadedAmethyst);
        color: var(--themeWhite);
    }
}
</style>