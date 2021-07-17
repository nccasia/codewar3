import { DanhMucSanPham } from './DanhMucSanPham'
export interface DanhMuc {
    DanhMucID: number;
    DanhMucPID: number;
    TenDanhMuc: string;
    GhiChu: string;
    DanhMucSanPham: DanhMucSanPham[];
}
