import request from '@/utils/request'
import { IDevice, IDevicesParams } from '@/views/device-management/types'

export const createDeviceApi = (data: IDevice) =>
  request({
    url: '/api/services/app/devices',
    method: 'post',
    data
  })

export const getDevicesApi = (data: IDevicesParams) =>
  request({
    url: '/api/services/app/devices/list',
    method: 'post',
    data
  })

export const getDeviceByIdApi = (id: string) =>
  request({
    url: `/api/services/app/devices/${id}`,
    method: 'get'
  })

export const deleteDeviceByIdApi = (id: string) =>
  request({
    url: `/api/services/app/devices/${id}`,
    method: 'delete'
  })
