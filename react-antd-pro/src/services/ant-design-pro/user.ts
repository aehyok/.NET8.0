import { request } from 'umi';
/** 用户信息保存 POST */
export async function addUser(options: any) {
  return request('/so/api/User/AddUser', {
    method: 'POST',
    data: options,
  });
}

/** 用户信息保存 POST */
export async function updateUser(options: any) {
  return request('/so/api/User/UpdateUser', {
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
