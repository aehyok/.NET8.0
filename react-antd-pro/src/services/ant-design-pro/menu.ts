import { request } from 'umi';

/** 通过字典类型获取字典项列表 GET /api/getUser */
export async function getMenuList() {
  return request<COMMON.ResultModel<SYSTEM.MenuItem[]>>('/api/getMenuList', {
    method: 'GET',
  });
}

/** 通过菜单id获取菜单详情 GET /api/getMenu */
export async function getMenu() {
  return request<COMMON.ResultModel<SYSTEM.MenuItem>>('/api/getMenu', {
    method: 'GET',
  });
}
