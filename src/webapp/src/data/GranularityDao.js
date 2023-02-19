import BaseDao from "./BaseDao.js"

export default class GranularityDao extends BaseDao {

    getGranularities() {
        return this.read(`/granularities`);
    }

    getGranularity(granularityId) {
        return this.read(`/granularity/${granularityId}`);
    }

    saveGranularity(granularity) {
        return this.write('/granularity', granularity);
    }
}