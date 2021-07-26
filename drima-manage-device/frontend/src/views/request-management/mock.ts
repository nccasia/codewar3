import {
  ERequestPriority,
  ERequestStatus,
  ERequestType,
  IDeviceRequest
} from './types'

export const DEVICE_REQUESTS: IDeviceRequest[] = [
  {
    id: '1',
    deviceName: 'Macbook Pro M1',
    type: ERequestType.ADDITION,
    description: 'Ram 16Gb, SSD 256, CPU Apple M1',
    reason:
      'I want to request a test device. It should be better if we have a Macbook Pro.',
    priority: ERequestPriority.MEDIUM,
    status: ERequestStatus.PENDING
  },
  {
    id: '3',
    deviceName: 'Macbook Pro M1',
    type: ERequestType.ADDITION,
    description: 'Ram 16Gb, SSD 256, CPU Apple M1',
    reason:
      'I want to request a test device. It should be better if we have a Macbook Pro.',
    priority: ERequestPriority.LOW,
    status: ERequestStatus.APPROVED
  },
  {
    id: '4',
    deviceName: 'Macbook Pro M1',
    type: ERequestType.ADDITION,
    description: 'Ram 16Gb, SSD 256, CPU Apple M1',
    reason:
      'I want to request a test device. It should be better if we have a Macbook Pro.',
    priority: ERequestPriority.CRITICAL,
    status: ERequestStatus.DELIVERED
  },
  {
    id: '5',
    deviceName: 'Macbook Pro M1',
    type: ERequestType.ADDITION,
    description: 'Ram 16Gb, SSD 256, CPU Apple M1',
    reason:
      'I want to request a test device. It should be better if we have a Macbook Pro.',
    priority: ERequestPriority.MEDIUM,
    status: ERequestStatus.REJECTED
  },
  {
    id: '2',
    deviceName: 'Macbook Pro M1',
    type: ERequestType.RETURN,
    deviceId: 'device1',
    status: ERequestStatus.PENDING
  },
  {
    id: '6',
    deviceName: 'Macbook Pro M1',
    type: ERequestType.RETURN,
    deviceId: 'device2',
    status: ERequestStatus.RETURNED
  }
]
