import { ERequestPriority, ERequestStatus } from './types'

export const requestStatusFilter = (status: ERequestStatus) => {
  switch (status) {
    case ERequestStatus.APPROVED:
      return 'Approved and Delivering'
    case ERequestStatus.DELIVERED:
      return 'Delivered'
    case ERequestStatus.PENDING:
      return 'Pending'
    case ERequestStatus.REJECTED:
      return 'Rejected'
    case ERequestStatus.RETURNED:
      return 'Returned'
    default:
      return ''
  }
}

export const requestPriorityFilter = (priority: ERequestPriority) => {
  switch (priority) {
    case ERequestPriority.LOW:
      return 'Low'
    case ERequestPriority.MEDIUM:
      return 'Medium'
    case ERequestPriority.CRITICAL:
      return 'Critical'
    default:
      return ''
  }
}
