export enum EDeviceStatus {
  AVAILABLE,
  USING,
  BROKEN
}

export interface IDevice {
  id?: string
  name: string
  description?: string
  manufacturer: string
  guaranteeDate?: Date
  status: EDeviceStatus
  borrowedUserId?: string
  borrowedDate?: Date
  qrCodeBase64?: string
}

export const DEVICE_STATUS_OPTIONS = [
  {
    label: 'Available',
    value: EDeviceStatus.AVAILABLE
  },
  {
    label: 'Broken',
    value: EDeviceStatus.BROKEN
  },
  {
    label: 'Using',
    value: EDeviceStatus.USING
  }
]

export interface IDevicesParams {
  sort?: string
  sortDirection?: number
  filterItems?: {
    propertyName: string
    value: any
  }[]
  searchText?: string
  skipCount: number
  maxResultCount: number
}

export const DEFAULT_FORM: IDevice = {
  name: '',
  description: '',
  manufacturer: '',
  guaranteeDate: new Date(),
  status: EDeviceStatus.AVAILABLE
}
