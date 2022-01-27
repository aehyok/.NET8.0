import { request } from '@/utils/request';
import MenuItem from '../../pages/editor/flow/components/EditorContextMenu/MenuItem';

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
