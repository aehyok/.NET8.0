// import { request } from '@/utils/request';

// /**
//  * 保存流程
//  * @param data
//  * @returns
//  */
// export async function SaveFlowEntityType(data: FLOW.FlowEntityType) {
//   return request<string>('/so/api/Flow/SaveFlowEntityType', {
//     method: 'POST',
//     data,
//   });
// }

import { request } from 'umi';

export async function addFlowEntityType(data: FLOW.FlowEntityType) {
  return request<any>('/so/api/Flow/AddFlowEntityType', {
    method: 'POST',
    data: data,
  });
}

export async function updateFlowEntityType(data: FLOW.FlowEntityType) {
  return request<any>('/so/api/Flow/UpdateFlowEntityType', {
    method: 'POST',
    data: data,
  });
}

export async function getFlowEntityType(id: string) {
  return request<FLOW.FlowEntityType>(`/so/api/Flow/GetFlowEntityType?flowId=${id}`, {
    method: 'GET',
  });
}
