import React, { useEffect, useState } from 'react';
import { Button } from 'antd';
import type { ProColumns } from '@ant-design/pro-table';
import ProTable from '@ant-design/pro-table';
import ProCard from '@ant-design/pro-card';
// @ts-ignore
import styles from './split.less';
import { request } from 'umi';
import { PageContainer } from '@ant-design/pro-layout';
import { getDictionaryList } from '@/services/ant-design-pro/api'



type DetailListProps = {
  typeCode: string;
};

const DictionaryList: React.FC<DetailListProps> = (props) => {
  const { typeCode } = props;
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
      // <><a key="a" >编辑</a><a key="b">删除</a><a key="c">禁用</a></>
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

export type DictionaryTypeItem = {
  typeCode?: string;
  name?: string;
};

const ipListDataSource: IpListItem[] = [];

for (let i = 0; i < 10; i += 1) {
  ipListDataSource.push({
    typeCode: `106.14.98.1${i}4`,
  });
}

type IPListProps = {
  typeCode: string;
  onChange: (id: string) => void;
};

const DictionaryType: React.FC<IPListProps> = (props) => {
  const { onChangeClick, typeCode } = props;

  const columns: ProColumns<IpListItem>[] = [
    {
      title: '字典类目',
      key: 'name',
      dataIndex: 'name',
    },
    {
      title: 'typeCode',
      key: 'typeCode',
      dataIndex: 'typeCode',
      hideInTable: true,
    },
    {
      title: '操作',
      valueType: 'option',
      render: () =>[
        <a
          key="editable"
          onClick={() => {

          }}
        >
          编辑
        </a>,
        <a
        key="deleteable"
        onClick={() => {

        }}
      >
        删除
      </a>,
      ],
    },
  ];
  return (
    <ProTable<IpListItem>
      columns={columns}
      request={(params, sorter, filter) => {
        // 表单搜索项会从 params 传入，传递给后端接口。
        console.log(params, sorter, filter);
        return request('/api/getDictionaryTypeList');
      }}
      rowKey="typeCode"
      rowClassName={(record) => {
        return record.typeCode === typeCode ? styles['split-row-select-active'] : '';
      }}
      toolbar={{
        search: {
          onSearch: (value) => {
            alert(value);
          },
        },
        actions: [
          <Button key="list" type="primary">
            新建
          </Button>,
        ],
      }}
      options={false}
      pagination={false}
      search={false}
      onRow={(record) => {
        return {
          onClick: () => {
            console.log('record', record)
            if (record.typeCode) {
              onChangeClick(record.typeCode);
            }
          },
        };
      }}
    />
  );
};

const DictionaryManage: React.FC = () => {
  const [typeCode, setTypeCode] = useState(undefined);
  return (
    <PageContainer>
      <ProCard split="vertical">
        <ProCard colSpan="324px" ghost>
          <DictionaryType onChangeClick={(typeCode: any) => setTypeCode(typeCode)} typeCode={typeCode} />
        </ProCard>
        <ProCard title={typeCode}>
          <DictionaryList typeCode={typeCode} />
        </ProCard>
      </ProCard>
    </PageContainer>
  );
};

export default DictionaryManage;
