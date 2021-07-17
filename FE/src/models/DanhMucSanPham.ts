import { SanPham } from './SanPham'
import { DanhMuc } from './DanhMuc'
export interface DanhMucSanPham {
  DanhMucSanPhamId: number;
  DanhMucId: number;
  SanPhamId: number;
  SanPham: SanPham;
  DanhMuc: DanhMuc;
}
