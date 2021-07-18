import { HTTP } from '@/HTTPServices'
import { BaseApi } from './BaseApi'
import { PaginatedResponse, Pagination } from './PaginatedResponse'
import { JobType } from '../models/JobType';
export interface JobTypeApiSearchParams extends Pagination {
  searchTerm?: string;
}
class JobTypeApi extends BaseApi {
  getAll (): Promise<JobType[]> {
    return new Promise<JobType[]>((resolve: any, reject: any) => {
      HTTP.get('jobtype').then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  getJobTypes (searchParams: JobTypeApiSearchParams): Promise<PaginatedResponse<JobType>> {
    return new Promise<PaginatedResponse<JobType>>((resolve: any, reject: any) => {
      HTTP.get('jobtype/jobtypes', {
        params: searchParams
      }).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  detail (id: number): Promise<JobType> {
    return new Promise<JobType>((resolve: any, reject: any) => {
      HTTP.get('jobtype/' + id).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  update (id: number, JobType: JobType): Promise<JobType> {
    return new Promise<JobType>((resolve: any, reject: any) => {
      HTTP.put('jobtype/' + id,
        JobType
      ).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  insert (JobType: JobType): Promise<JobType> {
    return new Promise<JobType>((resolve: any, reject: any) => {
      HTTP.post('jobtype',
        JobType
      ).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  delete (id: number): Promise<JobType> {
    return new Promise<JobType>((resolve: any, reject: any) => {
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
