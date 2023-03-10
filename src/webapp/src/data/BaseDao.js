import axios from 'axios';

export default class BaseDao {

    constructor(baseUrl) {
        this.baseUrl = baseUrl;
    }

    delete(command) {
        return new Promise((resolve, reject) => {
            const url = this.baseUrl + command;

            axios.delete(url)
                .then(res => {
                    resolve(res.data);
                })
                .catch(error => {
                    reject(error);
                })
        });
    }

    write(command, obj) {
        return new Promise((resolve, reject) => {
            const url = this.baseUrl + command;

            axios.post(url, obj)
                .then(res => {
                    resolve(res.data);
                })
                .catch(error => {
                    reject(error);
                })
        });
    }

    writeFile(command, formData) {
        return new Promise((resolve, reject) => {
            const url = this.baseUrl + command;

            const config = {
                headers: {
                    'content-type': 'multipart/form-data'
                }
            };

            axios.post(url, formData, config)
                .then(res => {
                    resolve(res.data);
                })
                .catch(error => {
                    reject(error);
                })
        });
    }

    read(command) {
        return new Promise((resolve, reject) => {
            const url = this.baseUrl + command;

            axios.get(url)
                .then(res => {
                    resolve(res.data);
                })
                .catch(error => {
                    reject(error);
                })
        });
    }

    readFile(command) {
        return new Promise((resolve, reject) => {
            const url = this.baseUrl + command;

            axios({
                method: 'get',
                url: url,
                responseType: 'blob'
            })
                .then(res => {
                    resolve(res);
                })
                .catch(error => {
                    reject(error);
                })
        });
    }
}