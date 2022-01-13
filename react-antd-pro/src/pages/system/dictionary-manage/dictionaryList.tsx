import React, { useEffect, useState } from 'react';
import { Button } from 'antd';
import { ProColumns } from '@ant-design/pro-table';
import ProTable from '@ant-design/pro-table';
// @ts-ignore
import { getDictionaryList } from '@/services/ant-design-pro/dictionary'



type ListProps = {
  typeCode: number| undefined;
  title: string | undefined;
};

const DictionaryList: React.FC<ListProps> = (props) => {
  const { typeCode, title } = props;
  console.log('--------DictionaryList--------',typeCode)
  const [tableListDataSource, setTableListDataSource] = useState<SYSTEM.DictionaryItem[]>([]);

  const columns: ProColumns<SYSTEM.DictionaryItem>[] = [
    {
      dataIndex: 'index',
      valueType: 'indexBorder',
      key:'index',
      width: 48,
    },
    {
      title: '名称',
      key: 'name',
      width: 80,
      dataIndex: 'name',
    },
    {
      title: '编码',
      key: 'code',
      width: 80,
      dataIndex: 'code',
    },
    {
      title: '排序',
      key: 'sequence',
      width: 80,
      dataIndex: 'sequence',
    },
    {
      title: '备注',
      key: 'remark',
      width: 80,
      dataIndex: 'remark',
    },
    {
      title: '状态',
      key: 'status',
      width: 80,
      dataIndex: 'status',
    },
    {
      title: '操作',
      key: 'option',
      width: 80,
      valueType: 'option',
      render: () => [
      <a
          key="edit"
          onClick={() => {

          }}
        >
          编辑
        </a>,
        <a
        key="delete"
        onClick={() => {

        }}
      >
        删除
      </a>,
    ],
    },
  ];

  useEffect(() => {
    const fetch = async() => {
      const source = await getDictionaryList()
      console.log(source, 'source')
      setTableListDataSource(source.data|| []);
    }

    fetch()
    console.log('fetch--request')

  }, [typeCode]);

  return (
    <ProTable<SYSTEM.DictionaryItem>
      columns={columns}
      dataSource={tableListDataSource}
      pagination={{
        pageSize: 10,
        showSizeChanger: false,
      }}
      rowKey="id"
      // toolBarRender={false}
      headerTitle={title}
      search={false}
      toolbar={{
        actions: [
          <Button key="lists" type="primary">
            新建
          </Button>,
        ],
      }}
    />
  );
};

export default DictionaryList
