import { HTTP } from '@/HTTPServices'
import { BaseApi } from './BaseApi'
import { PaginatedResponse, Pagination } from './PaginatedResponse'
import { KhachHang } from '@/models/KhachHang'
export interface KhachHangApiSearchParams extends Pagination {
    q?:string;
}
class KhachHangApi extends BaseApi {
  get (): Promise<KhachHang[]> {
    return new Promise<KhachHang[]>((resolve: any, reject: any) => {
      HTTP.get('KhachHang').then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  getDanhSach (searchParams: KhachHangApiSearchParams): Promise<PaginatedResponse<KhachHang>> {
    return new Promise<PaginatedResponse<KhachHang>>((resolve: any, reject: any) => {
      HTTP.get('KhachHang/danhsach', {
        params: searchParams
      }).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  detail (id: number): Promise<KhachHang> {
    return new Promise<KhachHang>((resolve: any, reject: any) => {
      HTTP.get('KhachHang/' + id).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  update (id: number, KhachHang: KhachHang): Promise<KhachHang> {
    return new Promise<KhachHang>((resolve: any, reject: any) => {
      HTTP.put('KhachHang/' + id,
        KhachHang
      ).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  insert (KhachHang: KhachHang): Promise<KhachHang> {
    return new Promise<KhachHang>((resolve: any, reject: any) => {
      HTTP.post('KhachHang',
        KhachHang
      ).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  delete (id: number): Promise<KhachHang> {
    return new Promise<KhachHang>((resolve: any, reject: any) => {
      HTTP.delete('KhachHang/' + id)
        .then((response) => {
          resolve(response.data)
        }).catch((error) => {
          reject(error)
        })
    })
  }
}
export default new KhachHangApi()
