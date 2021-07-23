import { HTTP } from '../http-services'

class JobTypeApi{
  getAll () {
    return new Promise((resolve, reject) => {
      HTTP.get('jobtype').then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  getJobTypes (searchParams) {
    return new Promise((resolve, reject) => {
      HTTP.get('jobtype/jobtypes', {
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
      HTTP.get('jobtype/' + id).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  update (id, JobType) {
    return new Promise((resolve, reject) => {
      HTTP.put('jobtype/' + id,
        JobType
      ).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  insert (JobType) {
    return new Promise((resolve, reject) => {
      HTTP.post('jobtype',
        JobType
      ).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }

  delete (id) {
    return new Promise((resolve, reject) => {
      HTTP.delete('jobtype/' + id)
        .then((response) => {
          resolve(response.data)
        }).catch((error) => {
          reject(error)
        })
    })
  }
}
export default new JobTypeApi()
