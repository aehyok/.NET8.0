import React, { useRef } from 'react';
import { PlusOutlined, EllipsisOutlined } from '@ant-design/icons';
import { Button, Tag, Space, Menu, Dropdown } from 'antd';
import type { ProColumns, ActionType } from '@ant-design/pro-table';
import ProTable, { TableDropdown } from '@ant-design/pro-table';
import request from 'umi-request';

type GithubIssueItem = {
  url: string;
  id: number;
  number: number;
  title: string;
  labels: {
    name: string;
    color: string;
  }[];
  state: string;
  comments: number;
  created_at: string;
  updated_at: string;
  closed_at?: string;
};

const columns: ProColumns<GithubIssueItem>[] = [
  {
    dataIndex: 'index',
    valueType: 'indexBorder',
    width: 48,
  },
  {
    title: '角色名称',
    dataIndex: 'title',
    ellipsis: true,
  },
  {
    title: '角色描述',
    dataIndex: 'state',
    filters: true,
    onFilter: true,
    valueType: 'select',
    valueEnum: {
      all: { text: '全部' },
      open: {
        text: '未解决',
      },
      closed: {
        text: '已解决',
      },
      processing: {
        text: '解决中',
      },
    },
  },
  {
    title: '排序',
    dataIndex: 'state',
    search: false,
  },
  {
    title: '创建时间',
    key: 'showTime',
    dataIndex: 'created_at',
    valueType: 'dateTime',
    sorter: true,
    hideInSearch: true,
  },
  {
    title: '更新时间',
    dataIndex: 'created_at',
    valueType: 'dateRange',
    hideInTable: true,
    search: {
      transform: (value) => {
        return {
          startTime: value[0],
          endTime: value[1],
        };
      },
    },
  },
  {
    title: '角色状态',
    dataIndex: 'state',
    filters: true,
    onFilter: true,
    valueType: 'select',
    valueEnum: {
      all: { text: '全部' },
      open: {
        text: '正常',
        status: 'success'
      },
      closed: {
        text: '禁用',
        status: 'error'
      },
      processing: {
        text: '解决中',
        status: 'error'
      },
    },
  },
  {
    title: '操作',
    valueType: 'option',
    render: (text, record, _, action) => [
      <a
        key="editable"
        onClick={() => {
          action?.startEditable?.(record.id);
        }}
      >
        编辑
      </a>,
      <a href={record.url} target="_blank" rel="noopener noreferrer" key="view">
        删除
      </a>,
      <a href={record.url} target="_blank" rel="noopener noreferrer" key="view">
        启用
      </a>
    ],
  },
];

const RoleInfo = () => {
  const actionRef = useRef<ActionType>();
  return (
    <ProTable<GithubIssueItem>
      columns={columns}
      actionRef={actionRef}
      request={async (params = {}, sort, filter) => {
        console.log(sort, filter);
        return request<{
          data: GithubIssueItem[];
        }>('https://proapi.azurewebsites.net/github/issues', {
          params,
        });
      }}
      rowKey="id"
      search={{
        labelWidth: 'auto',
      }}
      form={{
        // 由于配置了 transform，提交的参与与定义的不同这里需要转化一下
        syncToUrl: (values, type) => {
          if (type === 'get') {
            return {
              ...values,
              created_at: [values.startTime, values.endTime],
            };
          }
          return values;
        },
      }}
      pagination={{
        pageSize: 10,
      }}
      dateFormatter="string"
      headerTitle="角色列表"
      toolBarRender={() => [
        <Button key="button" icon={<PlusOutlined />} type="primary">
          新增
        </Button>,
      ]}
    />
  );
};

export default RoleInfo
