import { request } from 'umi';
/** 用户信息保存 POST */
export async function saveUser(options: any) {
  return request('/so/api/User/SaveUser', {
    method: 'POST',
    data: options,
  });
}

/** 删除用户信息POST */
export async function deleteUser(options: any) {
  return request('/so/api/User/DeleteUser?id=' + options.id, {
    method: 'POST',
  });
}

/** 删除用户信息POST */
export async function getUser(options: any) {
  return request('/so/api/User/GetUser?id=' + options.id, {
    method: 'GET',
  });
}
