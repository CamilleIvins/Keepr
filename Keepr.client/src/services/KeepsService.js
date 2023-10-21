import { AppState } from "../AppState.js"
import { api } from "./AxiosService.js"
import { Keep } from "../models/Keep.js"
import { logger } from '../utils/Logger.js';

class KeepsService {
async getKeeps(){
    const res = await api.get('api/keeps')
    // logger.log("Kservice GET")
    AppState.keeps = res.data.map(k => new Keep(k))
}
}

export const keepsService = new KeepsService()