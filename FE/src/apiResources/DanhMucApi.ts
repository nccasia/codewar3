import { HTTP } from '@/HTTPServices'
import { BaseApi } from './BaseApi'
import { PaginatedResponse, Pagination } from './PaginatedResponse'
import { DanhMuc } from '@/models/DanhMuc'
export interface DanhMucApiSearchParams extends Pagination {
    q?:string;
}
class DanhMucApi extends BaseApi {
  get (): Promise<DanhMuc[]> {
    return new Promise<DanhMuc[]>((resolve: any, reject: any) => {
      HTTP.get('DanhMuc').then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  getDanhSach (searchParams: DanhMucApiSearchParams): Promise<PaginatedResponse<DanhMuc>> {
    return new Promise<PaginatedResponse<DanhMuc>>((resolve: any, reject: any) => {
      HTTP.get('DanhMuc/danhsach', {
        params: searchParams
      }).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  getDanhSachCapNhat (DanhMucId: number): Promise<PaginatedResponse<DanhMuc>> {
    return new Promise<PaginatedResponse<DanhMuc>>((resolve: any, reject: any) => {
      HTTP.get('DanhMuc/danhsachcapnhat', {
        params: DanhMucId
      }).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  detail (id: number): Promise<DanhMuc> {
    return new Promise<DanhMuc>((resolve: any, reject: any) => {
      HTTP.get('DanhMuc/' + id).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  update (id: number, DanhMuc: DanhMuc): Promise<DanhMuc> {
    return new Promise<DanhMuc>((resolve: any, reject: any) => {
      HTTP.put('DanhMuc/' + id,
        DanhMuc
      ).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  insert (DanhMuc: DanhMuc): Promise<DanhMuc> {
    return new Promise<DanhMuc>((resolve: any, reject: any) => {
      HTTP.post('DanhMuc',
        DanhMuc
      ).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  delete (id: number): Promise<DanhMuc> {
    return new Promise<DanhMuc>((resolve: any, reject: any) => {
      HTTP.delete('DanhMuc/' + id)
        .then((response) => {
          resolve(response.data)
        }).catch((error) => {
          reject(error)
        })
    })
  }
}
export default new DanhMucApi()
