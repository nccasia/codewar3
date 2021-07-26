export enum ERequestType {
  ADDITION,
  RETURN
}

export enum ERequestPriority {
  LOW,
  MEDIUM,
  CRITICAL
}

export enum ERequestStatus {
  PENDING,
  APPROVED,
  REJECTED,
  DELIVERED,
  RETURNED
}

export interface IDeviceRequest {
  id: string
  type: ERequestType
  deviceName: string
  description?: string
  reason?: string
  priority?: ERequestPriority
  status?: ERequestStatus
  deviceId?: string
}

export const REQUEST_TYPE_OPTIONS = [
  {
    label: 'Addition',
    value: ERequestType.ADDITION
  },
  {
    label: 'Return',
    value: ERequestType.RETURN
  }
]

export const REQUEST_PRIORITY_OPTIONS = [
  {
    label: 'Low',
    value: ERequestPriority.LOW
  },
  {
    label: 'Medium',
    value: ERequestPriority.MEDIUM
  },
  {
    label: 'Critical',
    value: ERequestPriority.CRITICAL
  }
]

export interface IRequestsParams {
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
