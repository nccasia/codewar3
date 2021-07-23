import { HTTP } from '../http-services'

class JobApi {
  getAll() {
    return new Promise((resolve, reject) => {
      HTTP.get('job').then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }

  getJobs(searchParams) {
    return new Promise((resolve, reject) => {
      HTTP.get('job/jobs', {
        params: searchParams
      }).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }

  getUnregistedJob(searchParams) {
    return new Promise((resolve, reject) => {
      HTTP.get('job/unRegistedjobs', {
        params: searchParams
      }).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  getMyJob(searchParams) {
    return new Promise((resolve, reject) => {
      HTTP.get('job/myJob', {
        params: searchParams
      }).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
    
  approve (model) {
    return new Promise((resolve, reject) => {
      HTTP.post('job/approveJob',
        model
      ).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  getJobCandicate(searchParams) {
    return new Promise((resolve, reject) => {
      HTTP.get('job/candicate', {
        params: searchParams
      }).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  getAllJobCandicate(searchParams) {
    return new Promise((resolve, reject) => {
      HTTP.get('job/all-candicate', {
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
      HTTP.get('job/' + id).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  update (id, Job){
    return new Promise((resolve, reject) => {
      HTTP.put('job/' + id,
        Job
      ).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  insert (Job){
    return new Promise((resolve, reject) => {
      HTTP.post('job',
      Job
      ).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  registerJob (registerJob){
    return new Promise((resolve, reject) => {
      HTTP.post('job/registerJob',
      registerJob
      ).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  delete (id){
    return new Promise((resolve, reject) => {
      HTTP.delete('job/' + id)
        .then((response) => {
          resolve(response.data)
        }).catch((error) => {
          reject(error)
        })
    })
  }
}
export default new JobApi()
