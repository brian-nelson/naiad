import BaseDao from "./BaseDao.js"

export default class AuthDao extends BaseDao {

    login(email, password) {
        let user = {
            Email : email,
            Password : password
        };

        return this.write("/login", user);
    }
}