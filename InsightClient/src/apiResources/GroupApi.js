import { HTTP } from '../http-services'

class GroupApi{
  getAll () {
    return new Promise((resolve, reject) => {
      HTTP.get('group').then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  getGroups (searchParams) {
    return new Promise((resolve, reject) => {
      HTTP.get('group/groups', {
        params: searchParams
      }).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  getPermissions (searchParams) {
    return new Promise((resolve, reject) => {
      HTTP.get('group/permissions', {
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
      HTTP.get('group/' + id).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  update (id, Group) {
    return new Promise((resolve, reject) => {
      HTTP.put('group/' + id,
        Group
      ).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  insert (Group) {
    return new Promise((resolve, reject) => {
      HTTP.post('group',
        Group
      ).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  delete (id) {
    return new Promise((resolve, reject) => {
      HTTP.delete('group/' + id)
        .then((response) => {
          resolve(response.data)
        }).catch((error) => {
          reject(error)
        })
    })
  }
}
export default new GroupApi()
