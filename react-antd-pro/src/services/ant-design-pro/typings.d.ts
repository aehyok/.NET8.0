// @ts-ignore
/* eslint-disable */

declare namespace API {
  type CurrentUser = {
    name?: string;
    avatar?: string;
    userid?: string;
    email?: string;
    signature?: string;
    title?: string;
    group?: string;
    tags?: { key?: string; label?: string }[];
    notifyCount?: number;
    unreadCount?: number;
    country?: string;
    access?: string;
    geographic?: {
      province?: { label?: string; key?: string };
      city?: { label?: string; key?: string };
    };
    address?: string;
    phone?: string;
  };

  type LoginResult = {
    status?: string;
    type?: string;
    currentAuthority?: string;
  };

  type PageParams = {
    current?: number;
    pageSize?: number;
  };

  type RuleListItem = {
    key?: number;
    disabled?: boolean;
    href?: string;
    avatar?: string;
    name?: string;
    owner?: string;
    desc?: string;
    callNo?: number;
    status?: number;
    updatedAt?: string;
    createdAt?: string;
    progress?: number;
  };

  type RuleList = {
    data?: RuleListItem[];
    /** 列表的内容总数 */
    total?: number;
    success?: boolean;
  };

  type FakeCaptcha = {
    code?: number;
    status?: string;
  };

  type LoginParams = {
    username?: string;
    password?: string;
    autoLogin?: boolean;
    type?: string;
  };

  type ErrorResponse = {
    /** 业务约定的错误码 */
    errorCode: string;
    /** 业务上的错误信息 */
    errorMessage?: string;
    /** 业务上的请求是否成功 */
    success?: boolean;
  };

  type NoticeIconList = {
    data?: NoticeIconItem[];
    /** 列表的内容总数 */
    total?: number;
    success?: boolean;
  };

  type NoticeIconItemType = 'notification' | 'message' | 'event';

  type NoticeIconItem = {
    id?: string;
    extra?: string;
    key?: string;
    read?: boolean;
    avatar?: string;
    title?: string;
    status?: string;
    datetime?: string;
    description?: string;
    type?: NoticeIconItemType;
  };
}

// @ts-ignore
/* eslint-disable */

/**
 * 系统管理
 */
declare namespace SYSTEM {
  /**
   * 字典类型列表
   */
  type DictionaryTypeList = {
    code?: number;
    message?: string;
    data?: DictionaryTypeItem[];
  };

  /**
   * 字典类型项
   */
  type DictionaryTypeItem = {
    name?: string;
    typeCode?: number;
  };

  /**
   * 单个字典列表
   */
  type DictionaryList = {
    code?: number;
    message?: string;
    data?: DictionaryItem[];
  };

  /**
   * 字典中的某一项
   */
  type DictionaryItem = {
    id?: number;
    code?: string; // 编码
    name?: string; // 名称
    status?: Status; // 状态 0 禁用，1 正常
    sequence?: number; // 顺序
    remark?: string; // 备注
    iconFileUrl?: string; // 图标预览
    description?: string;
    fontColor?: string;
    iconFileId?: number;
    typeCode?: number;
  };

  /**
   * 菜单列表
   */
  type MenuList = {
    data?: MenuItem[];
    code?: number;
    message?: string;
  };

  type ResultItem = {
    data?: MenuItem;
    code?: number;
    message?: string;
  };

  type MenuItem = {
    action: number;
    biggest: number;
    children: MenuItem[];
    code: string;
    description: '';
    id: number;
    leaf: number;
    name: string;
    pcode: string;
    sequence: number;
    status: number;
    uiPath: string;
  };

  // 角色列表
  type RoleList = {
    code?: number;
    message?: string;
    data?: RoleItem[];
  };
  /**
   * 用户角色
   */
  type RoleItem = {
    dataAccess?: string;
    description?: string;
    id: number;
    name?: string;
    sequence?: number;
    status: number;
    type: number;
    updatedAt: Date;
    userTotal: number;
  };
}
