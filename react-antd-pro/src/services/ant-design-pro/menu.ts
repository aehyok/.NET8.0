import { request } from '@/utils/request';

/** 通过菜单id获取菜单详情 GET /api/getMenu */
export async function getMenu(id: string) {
  return request<SYSTEM.MenuItem>(`/so/api/Menu/getMenu?id=${id}`, {
    method: 'GET', });
}

export async function getMenuList() {
  return request<SYSTEM.MenuItem[]>(`/so/api/Menu/getMenuList`, {
    method: 'GET',
  });
}

export async function addMenu(data: SYSTEM.MenuItem) {
  return request<any>('/so/api/Menu/AddMenu', {
    method: 'POST',
    data: data,
  });
}

export async function updateMenu(data: SYSTEM.MenuItem) {
  return request<any>('/so/api/Menu/UpdateMenu', {
    method: 'POST',
    data: data,
  });
}

export async function deleteMenu(menuId: string) {
  return request<any>(`/so/api/Menu/DeleteMenu?menuId=${menuId}`, {
    method: 'POST',
  });
}
