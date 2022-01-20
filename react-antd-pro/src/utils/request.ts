import { request as requestApi } from 'umi';

/** 通用型request请求 */
export async function request<T>(url: string, options: any) {
  //return request<SYSTEM.DictionaryList>('/api/getDictionaryList', {
  return requestApi<COMMON.ResultModel<T>>(url, options);
}
