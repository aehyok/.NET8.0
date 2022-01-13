import React, { useRef, useState } from 'react';
import type { ActionType, ProColumns } from '@ant-design/pro-table';
import { EditableProTable } from '@ant-design/pro-table';
import ProCard from '@ant-design/pro-card';
import { ProFormField } from '@ant-design/pro-form';
import { Button, message, Modal } from 'antd';
import MenuModal from './modal'
import { request } from 'umi';
import { ExclamationCircleOutlined } from '@ant-design/icons';
import MenuItem from './../../editor/mind/components/EditorContextMenu/MenuItem';
import { PageContainer } from '@ant-design/pro-layout';

const waitTime = (time: number = 100) => {
  return new Promise((resolve) => {
    setTimeout(() => {
      resolve(true);
    }, time);
  });
};

type DataSourceType = {
  id?: React.Key;
  title?: string;
  decs?: string;
  state?: string;
  created_at?: string;
  update_at?: string;
  children?: DataSourceType[];
};

const defaultData: DataSourceType[] = [
  {
    id: 624748504,
    title: '活动名称一',
    decs: '这个活动真好玩',
    state: 'open',
    created_at: '2020-05-26T09:42:56Z',
    update_at: '2020-05-26T09:42:56Z',
    children: [
      {
        id: 62469122931,
        title: '活动名称二',
        decs: '这个活动真好玩',
        state: 'closed',
        created_at: '2020-05-26T08:19:22Z',
        update_at: '2020-05-26T08:19:22Z',
        children: [
          {
            id: 62469122932,
            title: '活动名称二',
            decs: '这个活动真好玩',
            state: 'closed',
            created_at: '2020-05-26T08:19:22Z',
            update_at: '2020-05-26T08:19:22Z',
            children: [
              {
                id: 62469122933,
                title: '活动名称二',
                decs: '这个活动真好玩',
                state: 'closed',
                created_at: '2020-05-26T08:19:22Z',
                update_at: '2020-05-26T08:19:22Z',
              },
            ],
          },
        ],
      },
    ],
  },
  {
    id: 624691229,
    title: '活动名称二',
    decs: '这个活动真好玩',
    state: 'closed',
    created_at: '2020-05-26T08:19:22Z',
    update_at: '2020-05-26T08:19:22Z',
  },
];

export default () => {

  const actionRef = useRef<ActionType>();
  const [isModalVisible, setIsModalVisible] = React.useState(false);
  const [editId, setEditId] = React.useState<number>()

  const [dataSource, setDataSource] = useState<DataSourceType[]>(() => defaultData);

  const editClick = (record: SYSTEM.MenuItem) => {
    console.log(record, '-------编辑-----------')
    setEditId(record.id)
    setIsModalVisible(true)
  }

  const addChildClick = (record: SYSTEM.MenuItem) => {
    console.log(record, '-----添加-----')
    setIsModalVisible(true)
  }

  const deleteClick = (record: SYSTEM.MenuItem) => {
    console.log(record, '-----删除---')
    Modal.confirm({
      title: '系统提示',
      icon: <ExclamationCircleOutlined />,
      content: '请确认是否删除该菜单（以及该指标节点下的指标）?',
      okText: '确认',
      onOk:() => {
        message.success('删除成功')
        actionRef.current?.reload()
      },
      onCancel : () => {console.log('取消')},
      cancelText: '取消',
    });

  }
  const columns: ProColumns<SYSTEM.MenuItem>[] = [
    {
      title: '菜单名称',
      dataIndex: 'name',
      width: '30%',
    },
    {
      title: '菜单路由',
      dataIndex: 'uiPath',
      width: '30%',
    },
    {
      title: 'CODE',
      dataIndex: 'code',
      width: '30%',
    },
    {
      title: '顺序',
      dataIndex: 'sequence',
      width: '30%',
    },
    {
      title: '状态',
      dataIndex: 'status',
      width: '30%',
    },
    {
      title: '操作',
      valueType: 'option',
      width: 200,
      render: (text, record) => [
        <a
          key="edit"
          onClick={ () => {editClick(record)}}
        >
          编辑
        </a>,
        <a
          key="删除"
          onClick={ () => {deleteClick(record)}}
        >
        删除
        </a>,
        <a
          key="addChild"
          onClick={ () => {addChildClick(record)}}
        >
          添加子菜单
        </a>,
      ],
    },
  ];

  const addRootMenuClick = () => {
    setIsModalVisible(true)
  }

  return (
    <>
      <PageContainer>
        <EditableProTable<SYSTEM.MenuItem>
          expandable={{
            // 使用 request 请求数据时无效
            defaultExpandAllRows: true,
          }}
          request={async (params = {}, sort, filter) => {
            console.log(sort, filter);
            return request<{
              data: SYSTEM.MenuItem[];
            }>('/api/getMenuList', {
              params,
            });
          }}
          actionRef={actionRef}
          rowKey="id"
          headerTitle="菜单树"
          recordCreatorProps={false}
          columns={columns}
          onChange={setDataSource}
          onRow={(row) => {
            return {
              onClick: () => {
                console.log('row', row)
              },
            }
          }}
          toolbar={{
            actions: [
              <Button key="lists" type="primary" onClick={() => {addRootMenuClick()}}>
                添加根菜单
              </Button>,
            ],
          }}
        />
        {
          !isModalVisible ? '' :
          <MenuModal modalVisible = {isModalVisible} hiddenModal = {setIsModalVisible} editId ={editId} actionRef={actionRef}/>
        }
      </PageContainer>
    </>
  );
};
