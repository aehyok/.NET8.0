import React, { useRef } from 'react';
import { ExclamationCircleOutlined, PlusOutlined } from '@ant-design/icons';
import { Button, message, Modal } from 'antd';
import type { ProColumns, ActionType } from '@ant-design/pro-table';
import ProTable from '@ant-design/pro-table';
import request from 'umi-request';
import { history } from 'umi';
import FlowModal from './modal';
import { deleteFlowEntityType } from '@/services/ant-design-pro/flow'
const FlowInfo = () => {
  const actionRef = useRef<ActionType>();
  const [isModalVisible, setIsModalVisible] = React.useState(false);
  const [editId, setEditId] = React.useState<string|undefined>(undefined)

  const jumpFlowDetail =(record: FLOW.FlowEntityType) => {
    console.log('11111111111', record)
    history.push(`/system/flow-detail?id=${record.id}`)
  }

  const editFlowTypeClick = (record: FLOW.FlowEntityType) => {
    console.log(record, '--record--')
    setEditId(record.id);
    setIsModalVisible(true)
  }

  const deleteFlowTypeClick = (record: FLOW.FlowEntityType) => {
    console.log(record, '--record--')
    Modal.confirm({
      title: '系统提示',
      icon: <ExclamationCircleOutlined />,
      content: '请确认是否删除该流程?',
      okText: '确认',
      onOk:() => {
        if(record && record.id) {
          deleteFlowEntityType(record.id).then((result: any) => {
            if(result.code === 200) {
              actionRef.current?.reload();
              message.success('删除成功！')
            }
          })
        }
      },
      onCancel : () => {console.log('取消')},
      cancelText: '取消',
    });
  }

  const operationFlowTypeClick = (record: FLOW.FlowEntityType) => {
    console.log(record, '--record--')
    Modal.confirm({
      title: '系统提示',
      icon: <ExclamationCircleOutlined />,
      content: '请确认是否启用该流程?',
      okText: '确认',
      onOk:() => {
        // if(record && record.id) {
        //   deleteFlowEntityType(record.id).then((result: any) => {
        //     if(result.code === 200) {
        //       actionRef.current?.reload();
        //       message.success('启用成功！')
        //     }
        //   })
        // }
      },
      onCancel : () => {console.log('取消')},
      cancelText: '取消',
    });
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
      render: (text: any,record: FLOW.FlowEntityType) => <a onClick={ ()=> {jumpFlowDetail(record)}}>{text}</a>,
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
            editFlowTypeClick(record)
          }}
        >
          编辑
        </a>,
        <a key="delete" onClick={() => {
          deleteFlowTypeClick(record)
        }}>
          删除
        </a>,
        <a  key="status" onClick={() => {
          operationFlowTypeClick(record)
        }}>
          { record.status === 0 ? '启用': '禁用'}
        </a>
      ],
    },
  ];

  const onAddFlowClick = () => {
    setEditId(undefined)
    setIsModalVisible(true)
  }
  return (
    <>
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
        <Button key="button" icon={<PlusOutlined />} type="primary" onClick={() => { onAddFlowClick() }}>
          新增
        </Button>,
      ]}
    />
    {
      !isModalVisible ? '' :
      <FlowModal modalVisible = {isModalVisible} hiddenModal = {setIsModalVisible} editId ={editId} actionRef={actionRef}/>
    }
    </>
  );
};

export default FlowInfo
