import { EDeviceStatus, IDevice } from './types'

export const DEVICE_LIST: IDevice[] = [
  {
    id: '1',
    name: 'Macbook Pro M1',
    description: 'Ram 16Gb, SSD 256, CPU Apple M1',
    status: EDeviceStatus.AVAILABLE,
    manufacturer: 'Apple',
    guaranteeDate: new Date(),
    borrowedDate: new Date(),
    borrowedUserId: '1'
  },
  {
    id: '1',
    name: 'Macbook Pro M1',
    description: 'Ram 16Gb, SSD 256, CPU Apple M1',
    status: EDeviceStatus.USING,
    manufacturer: 'Apple',
    guaranteeDate: new Date(),
    borrowedDate: new Date(),
    borrowedUserId: '1'
  },
  {
    id: '1',
    name: 'Macbook Pro M1',
    description: 'Ram 16Gb, SSD 256, CPU Apple M1',
    status: EDeviceStatus.BROKEN,
    manufacturer: 'Apple',
    guaranteeDate: new Date(),
    borrowedDate: new Date(),
    borrowedUserId: '1'
  }
]
