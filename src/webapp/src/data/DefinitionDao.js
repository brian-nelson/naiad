import BaseDao from "./BaseDao.js"

export default class DefinitionDao extends BaseDao {

  getDefinitions() {
    return this.read(`/definitions`);
  }

  getDefinition(name) {
    return this.read(`/definition/${name}`);
  }

  saveDefinition(definition) {
    return this.write('/definition', definition);
  }
}