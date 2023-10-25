<template>
    <section class="container-fluid" v-if="profile.id">
        <section class="row">
            <div class="about text-center">
                <img :src="profile.coverImg" alt="" class="elevation-4 profile-cover position-absolute">
                <div class="welcome font-dancing">Welcome,</div>
                <p class="welcome my-0 fs-3">my name is</p>
                <p class="welcome my-0 font-dancing">{{ profile.name.slice(0, profile.name.indexOf('@')) }}</p>
                <img class="rounded-circle profile-pic" :src="profile.picture" alt="" />
            </div>
            <div class="text-center mt-5">
                <p>#vault||#keep</p>
            </div>

        </section>
    </section>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, watchEffect } from 'vue';
import { useRoute } from 'vue-router';
import { logger } from '../utils/Logger.js';
import { profilesService } from '../services/ProfilesService.js';
import Pop from '../utils/Pop.js';
import { Keep } from '../models/Keep.js';
import { Profile } from '../models/Account.js';


export default {
    // props: { keep: { type: Keep || Object, required: true }, profile: { type: Profile || Object, required: true } },

    setup() {
        const route = useRoute()

        watchEffect(() => {
            getProfileById()
        })

        async function getProfileById() {
            try {
                const profileId = route.params.profileId
                await profilesService.getProfileById(profileId)
                logger.log(profileId)
            } catch (error) {
                logger.log(error)
                Pop.error(error)

            }
        }
        return {
            profile: computed(() => AppState.activeProfile)

        }
    }
};
</script>


<style lang="scss" scoped>
.welcome {
    position: relative;
    text-align: center;
    bottom: -5%;
    filter: drop-shadow(3px 8px 7px #292828);
    color: aliceblue;
    font-size: xxx-large;
    text-shadow: .5px .5px 1px black;


}

img.profile-cover {
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

@media screen and (min-width: 768px) {
    img.profile-cover {
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
}

.profile-pic {
    height: 20dvh;
    position: relative;
    bottom: -4vh;
    filter: drop-shadow(0 0 1px #ffffff);
    /* box-shadow: 0 3px 3px -1px rgba(205, 205, 205, 0.2),
    0 5px 6px 0 rgba(205, 205, 205, 0.14),
    0 1px 8px 0 rgba(205, 205, 205, 0.12); */
    box-shadow: 0 -0.5px 8px -0.5px #292828,
        0 -1px 8px -2px #292828;

}
</style>