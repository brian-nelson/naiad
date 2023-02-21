import BaseDao from "./BaseDao.js"

export default class DataFileDao extends BaseDao {

  listDataFiles(prefix) {
    if (prefix !== null) {
      return this.read(`/data/list/${prefix}`);
    }

    return this.read(`/data/list`);
  }
}