import { request } from 'umi';

/**
 * 获取当前指标下的下一级指标列表
 * @param id
 * @returns
 */
export async function GetChildGuideLines(id: string) {
  return request(`/so/api/Guideline/GetChildGuideLines?FatherGuildLineID=${id}`, {
    method: 'POST',
  });
}

/**
 * 通过指标ID获取指标名称
 * @param id
 * @returns
 */
export async function GetGuidelineDefine(id: string) {
  return request(`/so/api/Guideline/GetGuidelineDefine?guideLineId=${id}`, {
    method: 'POST',
  });
}

/**
 * 保存新的指标定义，主要先保存名称
 * @param data
 * @returns
 */
export async function InsertNewGuideLine(data: any) {
  return request(`/so/api/Guideline/InsertNewGuideLine?GuideLineName=${data.GuideLineName}&FatherID=${data.FatherID}`, {
    method: 'POST',
    data,
  });
}

/**
 * 保存新的指标定义，主要先保存名称
 * @param data
 * @returns
 */
 export async function SaveGuideLine(data: any) {
  return request(`/so/api/Guideline/SaveGuideLine`, {
    method: 'POST',
    data,
  });
}

/**
 * 通过id删除定义的指标
 * @param id
 * @returns
 */
export async function DelGuideLine(data: any) {
  return request(`/so/api/Guideline/DelGuideLine?GuideLineID=${data}`, {
    method: 'POST',
    data,
  });
}
