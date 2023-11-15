import { AppState } from "../AppState.js"
import { api } from "./AxiosService.js"
import { Profile } from "../models/Account.js"
import { logger } from '../utils/Logger.js';

class ProfilesService {
    async getProfileById(profileId){
        const res = await api.get(`api/profiles/${profileId}`)
        logger.log(res,"Kservice GET")
        AppState.activeProfile = new Profile(res.data)
    }
}

export const profilesService = new ProfilesService