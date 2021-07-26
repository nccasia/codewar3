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
  checkCandicate(JobK) {
    return new Promise((resolve, reject) => {
      var params = {JobK}
      HTTP.get('job/checkCandicate', {
        params: params
      }).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
    
  aprovedRegistrationJob (RegistrationJob) {
    return new Promise((resolve, reject) => {
      HTTP.post('job/aprovedregistrationJob',
      RegistrationJob
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
  getAllJobFinished(searchParams) {
    return new Promise((resolve, reject) => {
      HTTP.get('job/finishedJobs', {
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
  submit (Job){
    return new Promise((resolve, reject) => {
      HTTP.post('job/submitJob',
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
  approveFinishJob (Job){
    return new Promise((resolve, reject) => {
      HTTP.post('job/aprovedJob',
      Job
      ).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  declineFinishJob (Job, ReasonK){
    var prarams = {ReasonK}
    return new Promise((resolve, reject) => {
      HTTP.post('job/declinedJob',
      Job,{
        params: prarams
      }
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
