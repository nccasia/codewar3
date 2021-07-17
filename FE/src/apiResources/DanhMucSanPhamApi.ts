import { HTTP } from '@/HTTPServices'
import { BaseApi } from './BaseApi'
import { PaginatedResponse, Pagination } from './PaginatedResponse'
import { DanhMucSanPham } from '@/models/DanhMucSanPham'
export interface DanhMucSanPhamApiSearchParams extends Pagination {
    q?:string;
}
class DanhMucSanPhamApi extends BaseApi {
  search (searchParams: DanhMucSanPhamApiSearchParams): Promise<DanhMucSanPham[]> {
    return new Promise<DanhMucSanPham[]>((resolve: any, reject: any) => {
      HTTP.get('DanhMuc', {
        params: searchParams
      }).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  getDanhSachTheoSanPham (sanPhamId: number): Promise<number[]> {
    return new Promise<number[]>((resolve: any, reject: any) => {
      HTTP.get('DanhMucSanPham/danhsachtheosanpham', {
        params: {
          sanPhamId: sanPhamId
        }
      }).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  detail (id: number): Promise<DanhMucSanPham> {
    return new Promise<DanhMucSanPham>((resolve: any, reject: any) => {
      HTTP.get('DanhMucSanPham/' + id).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  update (id: number, DanhMucSanPham: DanhMucSanPham): Promise<DanhMucSanPham> {
    return new Promise<DanhMucSanPham>((resolve: any, reject: any) => {
      HTTP.put('DanhMucSanPham/' + id,
        DanhMucSanPham
      ).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  insert (DanhMucSanPham: DanhMucSanPham): Promise<DanhMucSanPham> {
    return new Promise<DanhMucSanPham>((resolve: any, reject: any) => {
      HTTP.post('DanhMucSanPham',
        DanhMucSanPham
      ).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  delete (id: number): Promise<DanhMucSanPham> {
    return new Promise<DanhMucSanPham>((resolve: any, reject: any) => {
      HTTP.delete('DanhMucSanPham/' + id)
        .then((response) => {
          resolve(response.data)
        }).catch((error) => {
          reject(error)
        })
    })
  }
}
export default new DanhMucSanPhamApi()
