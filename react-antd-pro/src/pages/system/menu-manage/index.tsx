import React, { useRef, useState } from 'react';
import type { ActionType, ProColumns } from '@ant-design/pro-table';
import { EditableProTable } from '@ant-design/pro-table';
import ProCard from '@ant-design/pro-card';
import { ProFormField } from '@ant-design/pro-form';
import { Button } from 'antd';
import MenuModal from './modal'
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

const loopDataSourceFilter = (
  data: DataSourceType[],
  id: React.Key | undefined,
): DataSourceType[] => {
  return data
    .map((item) => {
      if (item.id !== id) {
        if (item.children) {
          const newChildren = loopDataSourceFilter(item.children, id);
          return {
            ...item,
            children: newChildren.length > 0 ? newChildren : undefined,
          };
        }
        return item;
      }
      return null;
    })
    .filter(Boolean) as DataSourceType[];
};
export default () => {

  const actionRef = useRef<ActionType>();
  const [isModalVisible, setIsModalVisible] = React.useState(false);
  const [editId, setEditId] = React.useState(undefined)

  const [editableKeys, setEditableRowKeys] = useState<React.Key[]>([]);
  const [dataSource, setDataSource] = useState<DataSourceType[]>(() => defaultData);

  const [parentKey, setParentKey] = useState(undefined)
  const columns: ProColumns<DataSourceType>[] = [
    {
      title: '活动名称',
      dataIndex: 'title',
      formItemProps: (form, { rowIndex }) => {
        return {
          rules: rowIndex > 2 ? [{ required: true, message: '此项为必填项' }] : [],
        };
      },
      width: '30%',
    },
    {
      title: '状态',
      key: 'state',
      dataIndex: 'state',
      valueType: 'select',
      valueEnum: {
        all: { text: '全部', status: 'Default' },
        open: {
          text: '未解决',
          status: 'Error',
        },
        closed: {
          text: '已解决',
          status: 'Success',
        },
      },
    },
    {
      title: '描述',
      dataIndex: 'decs',
      fieldProps: (from, { rowKey, rowIndex }) => {
        if (from.getFieldValue([rowKey || '', 'title']) === '不好玩') {
          return {
            disabled: true,
          };
        }
        if (rowIndex > 9) {
          return {
            disabled: true,
          };
        }
        return {};
      },
    },
    {
      title: '活动时间',
      dataIndex: 'created_at',
      valueType: 'date',
    },
    {
      title: '操作',
      valueType: 'option',
      width: 200,
      render: (text, record) => [
        <a
          key="edit"
        >
          编辑
        </a>,
        <a
          key="删除"
        >
        删除
        </a>,
        <a
          key="addChild"
          onClick={() => {
            setDataSource(loopDataSourceFilter(dataSource, record.id));
          }}
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
      <EditableProTable<DataSourceType>
        expandable={{
          // 使用 request 请求数据时无效
          defaultExpandAllRows: true,
        }}
        actionRef={actionRef}
        rowKey="id"
        headerTitle="菜单树"
        // maxLength={5}
        recordCreatorProps={false}
        columns={columns}
        value={dataSource}
        onChange={setDataSource}
        onRow={(row) => {
          return {
            onClick: () => {
              console.log('row', row)
              setParentKey(row.id)
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
    </>
  );
};
