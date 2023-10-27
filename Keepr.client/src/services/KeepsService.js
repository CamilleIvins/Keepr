import { AppState } from "../AppState.js"
import { api } from "./AxiosService.js"
import { Keep } from "../models/Keep.js"
import { logger } from '../utils/Logger.js';

// TODO - CREATE (api.post, formData, AppState.push) && getByVK (api.get, res.data.map)
class KeepsService {

    async createKeep(newKeep){
        const res = await api.post(`api/keeps`, newKeep)
        const newestKeep =  new Keep(res.data)
        AppState.keeps.unshift(newestKeep)
        return newestKeep
    }

async getKeeps(){
    const res = await api.get('api/keeps')
    logger.log(res,"Kservice GET")
    AppState.keeps = res.data.map(k => new Keep(k))
}
// ID sets active Keep
async getKeepById(keepId){
const res = await api.get(`api/keeps/${keepId}`)
logger.log(res.data, "keep by Id")
const keep = new Keep(res.data)
// add/track views, if not creator
if(AppState.user != keep.creator){
    keep.views++
}
AppState.activeKeep = keep
}

// All Account Keeps
async getCreatorKeeps(profileId){
    const res = await api.get(`api/profiles/${profileId}/keeps`)
    logger.log(res.data,"Profile keeps GET")
    AppState.myKeeps = res.data.map(k=>new Keep(k))
}
// user NOT account
async getProfileKeeps(profileId){
    const res = await api.get(`api/profiles/${profileId}/keeps`)
    logger.log(res.data,"Profile keeps GET")
    AppState.profileKeeps = res.data.map(k=>new Keep(k))
}
// async createKeepModal(keepId){
//     const res = await api.get(`api/keeps/${keepId}`)
//     // res.data.id = AppState.keeps.find(keep=>keep.id==keepId)
// AppState.activeKeep = new Keep(res.data)
// }


async deleteKeep(keepId){
    await api.delete(`api/keeps/${keepId}`)
    const myUpdatedKeeps = AppState.myKeeps.filter(keep =>keep.id != keepId)
    logger.log(myUpdatedKeeps)
    const myProfileKeeps = AppState.profileKeeps.filter(keep =>keep.id != keepId)
    logger.log(myProfileKeeps)
}
}

export const keepsService = new KeepsService()

// Not getting keeps back, should have 18
//  {
//     "id": 18,
//     "creatorId": "650b29aecf2b0ac4e548c2e6",
//     "name": "Just Ok",
//     "description": "Bring to the table win-win survival strategies to ensure proactive domination.",
//     "views": 0,
//     "img": "https://images.unsplash.com/photo-1517423738875-5ce310acd3da?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=696&h=806&q=60",
//     "kept": 0,
//     "creator": {
//         "id": "650b29aecf2b0ac4e548c2e6",
//         "name": "quibbles@quibbles.com",
//         "picture": "https://unblob.blob.core.windows.net/authpics/CeasarZeppeli.png",
//         "coverImg": null
//     }
// }