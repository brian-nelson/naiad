import BaseDao from "./BaseDao.js"

export default class CollectionDao extends BaseDao {

  getCollections() {
    return this.read(`/collections`);
  }


}