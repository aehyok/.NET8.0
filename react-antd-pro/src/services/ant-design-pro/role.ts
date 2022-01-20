import { request } from '@/utils/request';

/** 通过字典类型获取字典项列表 GET /api/getUser */
export async function getRoleList() {
  return request<SYSTEM.RoleItem>('/api/getRoleList', {
    method: 'GET',
  });
}
