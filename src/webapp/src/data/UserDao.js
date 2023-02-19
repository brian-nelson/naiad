import BaseDao from "./BaseDao.js"

export default class UserDao extends BaseDao {

    getUsers() {
        return this.read(`/users`);
    }

    getUser(userId) {
        return this.read(`/users/${userId}`);
    }

    saveUser(user) {
        return this.write('/users', user);
    }

    changePassword(changePasswordRequest) {
        return this.write(`/user/password`, changePasswordRequest);
    }

    setPassword(userId, setPasswordRequest) {
        return this.write(`/users/${userId}/password`, setPasswordRequest);
    }
}