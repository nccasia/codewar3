import { HTTP } from '../http-services'

class UserApi{
  getAll () {
    return new Promise((resolve, reject) => {
      HTTP.get('user').then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  getUsers (searchParams) {
    return new Promise((resolve, reject) => {
      HTTP.get('user/users', {
        params: searchParams
      }).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  detail (id) {
    return new Promise((resolve, reject) => {
      HTTP.get('user/' + id).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  update (id, User) {
    return new Promise((resolve, reject) => {
      HTTP.put('user/' + id,
        User
      ).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  insert (User) {
    return new Promise((resolve, reject) => {
      HTTP.post('user',
        User
      ).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  delete (id) {
    return new Promise((resolve, reject) => {
      HTTP.delete('user/' + id)
        .then((response) => {
          resolve(response.data)
        }).catch((error) => {
          reject(error)
        })
    })
  }
}
export default new UserApi()
