import axios from 'axios';
import AuthDao from "../data/AuthDao";
import Environment from "../env";
import UserDao from "../data/UserDao";
import DefinitionDao from "../data/DefinitionDao";
import DataFileDao from "../data/DataFileDao";
import DataPointerDao from "../data/DataPointerDao";
import StructuredDataDao from "../data/StructuredDataDao";

export default class NaiadService {
    static JWT = "";
    static UserProperties = null;
    static UserAccess = {};

    static setJwt(jwt) {
        NaiadService.JWT = jwt;
        axios.defaults.headers.common['Authorization'] = 'Bearer ' + NaiadService.JWT;

        this.UserProperties = JSON.parse(atob(jwt.split('.')[1]));
    }

    static getCookie(cname) {
        let name = cname + "=";
        let decodedCookie = decodeURIComponent(document.cookie);
        let ca = decodedCookie.split(';');
        for(let i = 0; i <ca.length; i++) {
            let c = ca[i];
            while (c.charAt(0) === ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) === 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    }

    static setCookie(cname, cvalue, exdays) {
        let d = new Date();
        d.setTime(d.getTime() + (exdays*24*60*60*1000));
        let expires = "expires="+ d.toUTCString();
        document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
    }

    /* Auth */

    static login(username, password) {
        const dao = new AuthDao(Environment.BASE_URL);
        return dao.login(username, password);
    }

    static logout() {
        this.UserProperties = [];
        this.JWT = null;
        this.UserAccess = {};
    }

    /* Users */

    static getUsers() {
        const dao = new UserDao(Environment.BASE_URL);
        return dao.getUsers();
    }

    static getUser(userId) {
        const dao = new UserDao(Environment.BASE_URL);
        return dao.getUser(userId);
    }

    static saveUser(user) {
        const dao = new UserDao(Environment.BASE_URL);
        return dao.saveUser(user);
    }

    static setPassword(user, setPasswordRequest) {
        const dao = new UserDao(Environment.BASE_URL);
        return dao.setPassword(user, setPasswordRequest);
    }

    /* Data Definition */
    static getDefinitions() {
        const dao = new DefinitionDao(Environment.BASE_URL);
        return dao.getDefinitions();
    }

    static getDefinitionsByDataPointer(dataPointerId) {
        const dao = new DefinitionDao(Environment.BASE_URL);
        return dao.getDefinitionsByDataPointer(dataPointerId);
    }

    static getDefinition(name) {
        const dao = new DefinitionDao(Environment.BASE_URL);
        return dao.getDefinition(name);
    }

    static getDefinitionDetails(name) {
        const dao = new DefinitionDao(Environment.BASE_URL);
        return dao.getDefinitionDetails(name);
    }

    static saveDefinition(definition) {
        const dao = new DefinitionDao(Environment.BASE_URL);
        return dao.saveDefinition(definition);
    }

    static applyConvertor(dataPointerId, metadataId) {
        const dao = new DefinitionDao(Environment.BASE_URL);
        return dao.applyConverter(dataPointerId, metadataId);
    }

    /* Data Files */
    static listDataFiles(prefix) {
        const dao = new DataFileDao(Environment.BASE_URL);
        return dao.listDataFiles(prefix);
    }

    static downloadDataFile(filePath) {
        const dao = new DataFileDao(Environment.BASE_URL);
        return dao.getFile(filePath);
    }

    /* Data Pointer */
    static getDataPointer(dataPointerId) {
        const dao = new DataPointerDao(Environment.BASE_URL);
        return dao.getDataPointer(dataPointerId);
    }

    /* Structured Data */
    static getStructuredData(metadataId, skip, limit) {
        const dao = new StructuredDataDao(Environment.BASE_URL);
        return dao.getStructuredData(metadataId, skip, limit);
    }
}