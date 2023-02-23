import BaseDao from "./BaseDao.js"

export default class DataPointerDao extends BaseDao {

  getDataPointer(dataPointerId) {
    return this.read(`/datapointer/${dataPointerId}`);
  }
}