import { request } from 'umi';

/**
 * 获取所有form表单
 * @param id
 * @returns
 */
export async function getSystemFormList() {
  return request(`/so/api/Form/GetSystemFormList`, {
    method: 'GET',
  });
}

/**
 * 通过表单ID获取表单名称
 * @param id
 * @returns
 */
export async function getSystemForm(id: string) {
  return request(`/so/api/Form/getSystemForm?id=${id}`, {
    method: 'GET',
  });
}

/**
 * 保存新的表单定义，主要先保存名称
 * @param data
 * @returns
 */
export async function insertSystemForm(data: any) {
  return request(`/so/api/Form/InsertSystemForm`, {
    method: 'POST',
    data,
  });
}

/**
 * 修改form表单
 * @param data
 * @returns
 */
export async function updateSystemForm(data: any) {
  return request(`/so/api/Form/UpdateSystemForm`, {
    method: 'POST',
    data,
  });
}

/**
 * 通过id删除定义的表单
 * @param id
 * @returns
 */
export async function deleteSystemForm(data: any) {
  return request(`/so/api/Form/DeleteSystemForm?id=${data}`, {
    method: 'POST',
    data,
  });
}
