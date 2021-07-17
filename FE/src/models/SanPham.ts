import { DanhMucSanPham } from './DanhMucSanPham'
export interface SanPham {
  SanPhamId:number;
  TenSanPham:string;
  ThongTinSanPham:string;
  GhiChu:string;
  DanhMucSanPham:DanhMucSanPham[];
}
