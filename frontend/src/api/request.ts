import request from '@/utils/request'
import { IDeviceRequest, IRequestsParams } from '@/views/request-management/types'

export const createRequestApi = (data: IDeviceRequest) =>
  request({
    url: '/api/services/app/requests',
    method: 'post',
    data
  })

export const getRequestsApi = (data: IRequestsParams) =>
  request({
    url: '/api/services/app/requests/list',
    method: 'post',
    data
  })

export const getRequestByIdApi = (id: string) =>
  request({
    url: `/api/services/app/requests/${id}`,
    method: 'get'
  })

export const deleteRequestByIdApi = (id: string) =>
  request({
    url: `/api/services/app/requests/${id}`,
    method: 'delete'
  })
