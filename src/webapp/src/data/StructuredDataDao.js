import BaseDao from "./BaseDao.js"

export default class StructuredDataDao extends BaseDao {
  getStructuredData(metadataId, skip, limit) {
    return this.read(`/structured/${metadataId}/${skip}/${limit}`);
  }
}