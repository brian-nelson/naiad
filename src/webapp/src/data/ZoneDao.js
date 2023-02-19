import BaseDao from "./BaseDao.js"

export default class ZoneDao extends BaseDao {

    getZones() {
        return this.read(`/zones`);
    }

    getZone(zoneId) {
        return this.read(`/zone/${zoneId}`);
    }

    saveZone(zone) {
        return this.write('/zones', zone);
    }
}