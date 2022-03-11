import { request } from '@/utils/request';
import type { SYSTEM } from '@/services/ant-design-pro/typings'

export async function getDictionaryTypeList() {
  return request<SYSTEM.DictionaryTypeItem[]>('/so/api/Dictionary/getDictionaryTypeList', {
    method: 'GET',
  });
}

export async function getDictionaryType<T>(id: string) {
  return request<T>(`/so/api/Dictionary/getDictionaryType?dictionaryTypeId=${id}`, {
    method: 'GET', });
}

export async function addDictionaryType(data: SYSTEM.DictionaryTypeItem) {
  return request<any>('/so/api/Dictionary/AddDictionaryType', {
    method: 'POST',
    data: data,
  });
}

export async function updateDictionaryType(data: SYSTEM.DictionaryTypeItem) {
  return request<any>('/so/api/Dictionary/UpdateDictionaryType', {
    method: 'POST',
    data: data,
  });
}

export async function deleteDictionaryType(dictionaryTypeId: string) {
  return request<any>(`/so/api/Dictionary/DeleteDictionaryType?dictionaryTypeId=${dictionaryTypeId}`, {
    method: 'POST',
  });
}

export async function getDictionaryList(typeCode: string) {
  return request<SYSTEM.DictionaryItem[]>(`/so/api/Dictionary/GetDictionaryList?typeCode=${typeCode}`, {
    method: 'GET',
  });
}

export async function getDictionary(id: string) {
  return request<SYSTEM.DictionaryItem>(`/so/api/Dictionary/GetDictionary?dictionaryId=${id}`, {
    method: 'GET', });
}

export async function addDictionary(data: SYSTEM.DictionaryItem) {
  return request<any>('/so/api/Dictionary/AddDictionary', {
    method: 'POST',
    data: data,
  });
}

export async function updateDictionary(data: SYSTEM.DictionaryItem) {
  return request<any>('/so/api/Dictionary/UpdateDictionary', {
    method: 'POST',
    data: data,
  });
}

export async function deleteDictionary(dictionaryId: string) {
  return request<any>(`/so/api/Dictionary/DeleteDictionary?dictionaryId=${dictionaryId}`, {
    method: 'POST',
  });
}
