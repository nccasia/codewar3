import { HTTP } from '@/HTTPServices'
import { BaseApi } from './BaseApi'
import { PaginatedResponse, Pagination } from './PaginatedResponse'
import { Phong } from '@/models/Phong'
export interface PhongApiSearchParams extends Pagination {
}
class PhongApi extends BaseApi {
  search (searchParams: PhongApiSearchParams): Promise<PaginatedResponse<Phong>> {
    return new Promise<PaginatedResponse<Phong>>((resolve: any, reject: any) => {
      HTTP.get('phong', {
        params: searchParams
      }).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  detail (id: number): Promise<Phong> {
    return new Promise<Phong>((resolve: any, reject: any) => {
      HTTP.get('phong/' + id).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  update (id: number, phong: Phong): Promise<Phong> {
    return new Promise<Phong>((resolve: any, reject: any) => {
      HTTP.put('phong/' + id,
        phong
      ).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  insert (phong: Phong): Promise<Phong> {
    return new Promise<Phong>((resolve: any, reject: any) => {
      HTTP.post('phong',
        phong
      ).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  delete (id: number): Promise<Phong> {
    return new Promise<Phong>((resolve: any, reject: any) => {
      HTTP.delete('phong/' + id)
        .then((response) => {
          resolve(response.data)
        }).catch((error) => {
          reject(error)
        })
    })
  }
}
export default new PhongApi()
