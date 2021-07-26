import { HTTP } from '../http-services'

class WorkflowStatusApi {
  getAll() {
    return new Promise((resolve, reject) => {
      HTTP.get('workflowstatus').then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  getWFStatuss (workflowstatues) {
    return new Promise((resolve, reject) => {
      HTTP.get('workflowstatus/workflowstatues', {
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
      HTTP.get('workflowstatus/' + id).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  update (id, Job){
    return new Promise((resolve, reject) => {
      HTTP.put('workflowstatus/' + id,
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
      HTTP.post('workflowstatus',
      Job
      ).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  delete (id){
    return new Promise((resolve, reject) => {
      HTTP.delete('workflowstatus/' + id)
        .then((response) => {
          resolve(response.data)
        }).catch((error) => {
          reject(error)
        })
    })
  }
}
export default new WorkflowStatusApi()
