import { request } from '@/utils/request';

/** 通过菜单id获取菜单详情 GET /api/getMenu */
export async function getMenu() {
  return request<SYSTEM.MenuItem>('/so/api/Menu/getMenu', {
    method: 'GET',
  });
}

export async function getMenuList() {
  return request<SYSTEM.MenuItem[]>(`/so/api/Menu/getMenuList`, {
    method: 'GET',
  });
}
