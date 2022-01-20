import { request } from 'umi';

/** 通过字典类型获取字典项列表 GET /api/getUser */
export async function getDictionaryList() {
  //return request<SYSTEM.DictionaryList>('/api/getDictionaryList', {
  return request<COMMON.ResultModel<SYSTEM.DictionaryItem[]>>('/api/getDictionaryList', {
    method: 'GET',
  });
}

/** 通过菜单id获取菜单详情 GET /api/getMenu */
export async function getDictionaryTypeList() {
  return request<COMMON.ResultModel<SYSTEM.DictionaryTypeItem[]>>('/api/getDictionaryTypeList', {
    method: 'GET',
  });
}
