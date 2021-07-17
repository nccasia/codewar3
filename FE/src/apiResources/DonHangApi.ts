import { HTTP } from '@/HTTPServices'
import { BaseApi } from './BaseApi'
import { PaginatedResponse, Pagination } from './PaginatedResponse'
import { DonHang } from '@/models/DonHang'
export interface DonHangApiSearchParams extends Pagination {
    q?:string;
}
class DonHangApi extends BaseApi {
  get (): Promise<DonHang[]> {
    return new Promise<DonHang[]>((resolve: any, reject: any) => {
      HTTP.get('DonHang').then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  getDanhSach (searchParams: DonHangApiSearchParams): Promise<PaginatedResponse<DonHang>> {
    return new Promise<PaginatedResponse<DonHang>>((resolve: any, reject: any) => {
      HTTP.get('DonHang/danhsach', {
        params: searchParams
      }).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  detail (id: number): Promise<DonHang> {
    return new Promise<DonHang>((resolve: any, reject: any) => {
      HTTP.get('DonHang/' + id).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  update (id: number, DonHang: DonHang): Promise<DonHang> {
    return new Promise<DonHang>((resolve: any, reject: any) => {
      HTTP.put('DonHang/' + id,
        DonHang
      ).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  insert (DonHang: DonHang): Promise<DonHang> {
    return new Promise<DonHang>((resolve: any, reject: any) => {
      HTTP.post('DonHang',
        DonHang
      ).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  delete (id: number): Promise<DonHang> {
    return new Promise<DonHang>((resolve: any, reject: any) => {
      HTTP.delete('DonHang/' + id)
        .then((response) => {
          resolve(response.data)
        }).catch((error) => {
          reject(error)
        })
    })
  }
}
export default new DonHangApi()
