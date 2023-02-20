import BaseDao from "./BaseDao.js"

export default class UserDao extends BaseDao {

    getUsers() {
        return this.read(`/users`);
    }

    getUser(userId) {
        return this.read(`/user/${userId}`);
    }

    saveUser(user) {
        return this.write('/user', user);
    }

    changePassword(changePasswordRequest) {
        return this.write(`/user/password`, changePasswordRequest);
    }

    setPassword(userId, setPasswordRequest) {
        return this.write(`/user/${userId}/password`, setPasswordRequest);
    }
}