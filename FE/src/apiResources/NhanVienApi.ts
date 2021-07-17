import { HTTP } from '@/HTTPServices'
import { BaseApi } from './BaseApi'
import { PaginatedResponse, Pagination } from './PaginatedResponse'
import { NhanVien } from '@/models/NhanVien'
export interface NhanVienApiSearchParams extends Pagination {
    q?:string;
}
class NhanVienApi extends BaseApi {
  get (): Promise<NhanVien[]> {
    return new Promise<NhanVien[]>((resolve: any, reject: any) => {
      HTTP.get('NhanVien').then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  getDanhSach (searchParams: NhanVienApiSearchParams): Promise<PaginatedResponse<NhanVien>> {
    return new Promise<PaginatedResponse<NhanVien>>((resolve: any, reject: any) => {
      HTTP.get('NhanVien/danhsach', {
        params: searchParams
      }).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  detail (id: number): Promise<NhanVien> {
    return new Promise<NhanVien>((resolve: any, reject: any) => {
      HTTP.get('NhanVien/' + id).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  update (id: number, NhanVien: NhanVien): Promise<NhanVien> {
    return new Promise<NhanVien>((resolve: any, reject: any) => {
      HTTP.put('NhanVien/' + id,
        NhanVien
      ).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  insert (NhanVien: NhanVien): Promise<NhanVien> {
    return new Promise<NhanVien>((resolve: any, reject: any) => {
      HTTP.post('NhanVien',
        NhanVien
      ).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  delete (id: number): Promise<NhanVien> {
    return new Promise<NhanVien>((resolve: any, reject: any) => {
      HTTP.delete('NhanVien/' + id)
        .then((response) => {
          resolve(response.data)
        }).catch((error) => {
          reject(error)
        })
    })
  }
}
export default new NhanVienApi()
