import { HTTP } from '../http-services'
class ListCategoryApi {
  getAllJobStatuses () {
    return new Promise((resolve, reject) => {
      HTTP.get('listcategory/satatustypes', {
      }).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  getAllJobTypes () {
    return new Promise((resolve, reject) => {
      HTTP.get('listcategory/jobtypes', {
      }).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  getRejectReason () {
    return new Promise((resolve, reject) => {
      HTTP.get('listcategory/jobRejectReasons', {
      }).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
}
export default new ListCategoryApi()
