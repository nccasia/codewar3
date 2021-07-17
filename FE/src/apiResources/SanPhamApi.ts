import { HTTP } from '@/HTTPServices'
import { BaseApi } from './BaseApi'
import { PaginatedResponse, Pagination } from './PaginatedResponse'
import { AnhSanPham } from '@/models/AnhSanPham'
import { ThongSoSanPham } from '@/models/ThongSoSanPham'
import { SanPham } from '@/models/SanPham'
export interface SanPhamApiSearchParams extends Pagination {
    q?:string;
}
class SanPhamApi extends BaseApi {
  search (searchParams: SanPhamApiSearchParams): Promise<PaginatedResponse<SanPham>> {
    return new Promise<PaginatedResponse<SanPham>>((resolve: any, reject: any) => {
      HTTP.get('SanPham', {
        params: searchParams
      }).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  getDanhSach (searchParams: SanPhamApiSearchParams): Promise<PaginatedResponse<SanPham>> {
    return new Promise<PaginatedResponse<SanPham>>((resolve: any, reject: any) => {
      HTTP.get('SanPham/danhsach', {
        params: searchParams
      }).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  getDanhSachThongSoSanPham (sanPhamId: number): Promise<ThongSoSanPham[]> {
    return new Promise<ThongSoSanPham[]>((resolve: any, reject: any) => {
      HTTP.get('SanPham/thongsosanpham', {
        params: { sanPhamId: sanPhamId }
      }).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  getAnhSanPham (sanPhamId: number): Promise<AnhSanPham[]> {
    return new Promise<AnhSanPham[]>((resolve: any, reject: any) => {
      HTTP.get('SanPham/anhsanpham', {
        params: { sanPhamId: sanPhamId }
      }).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  detail (id: number): Promise<SanPham> {
    return new Promise<SanPham>((resolve: any, reject: any) => {
      HTTP.get('SanPham/' + id).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  update (id: number, SanPham: SanPham): Promise<SanPham> {
    return new Promise<SanPham>((resolve: any, reject: any) => {
      HTTP.put('SanPham/' + id,
        SanPham
      ).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  insert (SanPham: SanPham): Promise<SanPham> {
    return new Promise<SanPham>((resolve: any, reject: any) => {
      HTTP.post('SanPham',
        SanPham
      ).then((response) => {
        resolve(response.data)
      }).catch((error) => {
        reject(error)
      })
    })
  }
  delete (id: number): Promise<SanPham> {
    return new Promise<SanPham>((resolve: any, reject: any) => {
      HTTP.delete('SanPham/' + id)
        .then((response) => {
          resolve(response.data)
        }).catch((error) => {
          reject(error)
        })
    })
  }
}
export default new SanPhamApi()
