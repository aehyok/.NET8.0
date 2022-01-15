import React, { useRef } from 'react';
import { ExclamationCircleOutlined, PlusOutlined } from '@ant-design/icons';
import { Button, Tag, Space, Skeleton, Modal, message } from 'antd';
import type { ProColumns, ActionType } from '@ant-design/pro-table';
import ProTable from '@ant-design/pro-table';
import request from 'umi-request';
import { PageContainer } from '@ant-design/pro-layout';
import UserModal from './modal'
import type { UserItem } from './type.d'
import { deleteUser } from '@/services/ant-design-pro/user'

export default () => {
  const actionRef = useRef<ActionType>();
  const [isModalVisible, setIsModalVisible] = React.useState(false);
  const [editId, setEditId] = React.useState(undefined)
  const addUserClick = () => {
    setEditId(undefined)
    setIsModalVisible(true)
  }

  const editClick = (record: any) => {
    setEditId(record.id)
    setIsModalVisible(true)
  }

  const deleteClick = (record: any) => {
    Modal.confirm({
      title: '系统提示',
      icon: <ExclamationCircleOutlined />,
      content: '请确认是否删除该指标（以及该指标节点下的指标）?',
      okText: '确认',
      onOk: async() => {
        const result = await deleteUser({
            id: record.id
          }
        )
        if(result.code === 200) {
          message.success('删除成功')
          actionRef.current?.reload()
        }
      },
      onCancel : () => {console.log('取消')},
      cancelText: '取消',
    });
  }
  const columns: ProColumns<UserItem>[] = [
    {
      dataIndex: 'index',
      valueType: 'indexBorder',
      width: 48,
    },
    {
      title: '姓名',
      dataIndex: 'nickName',
      formItemProps: {
        rules: [
          {
            required: true,
            message: '请输入姓名',
          },
        ],
      },
    },
    {
      title: '账号',
      dataIndex: 'account',
      formItemProps: {
        rules: [
          {
            required: true,
            message: '请输入账号',
          },
        ],
      },
    },
    {
      title: '性别',
      dataIndex: 'sex',
      filters: true,
      onFilter: true,
      valueType: 'select',
      valueEnum: {
        0: '未知',
        1: '男',
        2: '女'
      },
    },
    {
      title: '角色',
      dataIndex: 'roleInfo',
      valueType: 'select',
      valueEnum: {
        1: '管理员',
        2: '普通用户'
      },
    },
    {
      title: '创建时间',
      dataIndex: 'createdAt',
      valueType: 'dateTime',
      sorter: true,
      // hideInSearch: true,
    },
    {
      title: '修改时间',
      dataIndex: 'updatedAt',
      // hideInTable: true,
    },
    {
      title: '操作',
      valueType: 'option',
      render: (text, record, _, action) => [
        <a
          key="editable"
          onClick={() => {
            //action?.startEditable?.(record.id);
            editClick(record)
          }}
        >
          编辑
        </a>,
        <a  key="view"
        onClick={() => {
          //action?.startEditable?.(record.id);
          deleteClick(record)
        }}
        >
          删除
        </a>,
      ],
    },
  ];

  return (
    <PageContainer>
      <ProTable<UserItem>
        columns={columns}
        actionRef={actionRef}
        request={async (params = {}, sort, filter) => {
          console.log(sort, filter);
          return request<{
            data: UserItem[];
          }>('/so/api/User/getUserList', {
            params,
          });
        }}
        editable={{
          type: 'multiple',
        }}
        columnsState={{
          persistenceKey: 'pro-table-singe-demos',
          persistenceType: 'localStorage',
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
        toolBarRender={() => [
          <Button key="button" icon={<PlusOutlined />} type="primary" onClick={ () => addUserClick()}>
            新增
          </Button>,
        ]}
      />
      {
        !isModalVisible ? '' :
        <UserModal modalVisible = {isModalVisible} hiddenModal = {setIsModalVisible} editId ={editId} actionRef={actionRef}/>
      }
    </PageContainer>
  );
};
