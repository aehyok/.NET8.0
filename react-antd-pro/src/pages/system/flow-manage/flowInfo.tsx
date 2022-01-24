import React, { useRef } from 'react';
import { PlusOutlined, EllipsisOutlined } from '@ant-design/icons';
import { Button, Tag, Space, Menu, Dropdown } from 'antd';
import type { ProColumns, ActionType } from '@ant-design/pro-table';
import ProTable, { TableDropdown } from '@ant-design/pro-table';
import request from 'umi-request';
import { history } from 'umi';

const jumpFlowDetail =(record: any) => {
  console.log('11111111111', record)
  history.push('/system/flow-detail')
}

const columns: ProColumns<FLOW.FlowEntityType>[] = [
  {
    dataIndex: 'index',
    valueType: 'indexBorder',
    width: 48,
  },
  {
    title: '流程名称',
    dataIndex: 'flowName',
    ellipsis: true,
    render: (text: any,record: any) => <a onClick={ ()=> {jumpFlowDetail(record)}}>{text}</a>,
  },
  {
    title: '流程说明',
    dataIndex: 'description',
    filters: true,
  },
  {
    title: '排序',
    dataIndex: 'displayOrder',
    search: false,
  },
  {
    title: '状态',
    dataIndex: 'status',
    search: false,
  },
  {
    title: '创建时间',
    key: 'showTime',
    dataIndex: 'created_at',
    valueType: 'dateTime',
    sorter: true,
    // hideInSearch: true,
  },
  {
    title: '更新时间',
    dataIndex: 'created_at',
    valueType: 'dateRange',
    // hideInTable: true,
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
    <ProTable<FLOW.FlowEntityType>
      columns={columns}
      actionRef={actionRef}
      request={async (params = {}, sort, filter) => {
        console.log(sort, filter);
        return request<{
          data: FLOW.FlowEntityType[];
        }>('/so/api/Flow/GetFlowEntityTypeList', {
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
      headerTitle="流程列表"
      toolBarRender={() => [
        <Button key="button" icon={<PlusOutlined />} type="primary">
          新增
        </Button>,
      ]}
    />
  );
};

export default RoleInfo
