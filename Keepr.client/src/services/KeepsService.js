import { AppState } from "../AppState.js"
import { api } from "./AxiosService.js"
import { Keep } from "../models/Keep.js"
import { logger } from '../utils/Logger.js';

class KeepsService {
async getKeeps(){
    const res = await api.get('api/keeps')
    logger.log(res,"Kservice GET")
    AppState.keeps = res.data.map(k => new Keep(k))
}
async getKeepById(keepId){
const res = await api.get(`api/keeps/${keepId}`)
logger.log(res.data, "keep by Id")
const keep = new Keep(res.data)
AppState.activeKeep = keep
}

async getCreatorKeeps(profileId){
    const res = await api.get(`api/profiles/${profileId}/keeps`)
    logger.log(res.data,"Profile keeps GET")
    AppState.myKeeps = res.data.map(k=>new Keep(k))
}
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