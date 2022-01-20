import { request } from 'umi';

/** 通过字典类型获取字典项列表 GET /api/getUser */
export async function getRoleList() {
  return request<COMMON.ResultModel<SYSTEM.RoleItem>>('/api/getRoleList', {
    method: 'GET',
  });
}
