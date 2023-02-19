import BaseDao from "./BaseDao.js"

export default class CategorizationDao extends BaseDao {

    getCategorizations() {
        return this.read(`/categorizations`);
    }

    getCategorization(categorizationId) {
        return this.read(`/categorization/${categorizationId}`);
    }

    saveCategorization(categorization) {
        return this.write('/categorization', categorization);
    }
}