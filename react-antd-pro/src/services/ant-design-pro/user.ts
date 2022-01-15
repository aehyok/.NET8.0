import { request } from 'umi';
/** 用户信息保存 POST */
export async function saveUser(options: any) {
  return request('/so/api/User/SaveUser', {
    method: 'POST',
    data: options,
  });
}
