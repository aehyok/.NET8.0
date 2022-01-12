import React, { useEffect, useState } from 'react';
import type { BadgeProps } from 'antd';
import { Button, Badge } from 'antd';
import type { ProColumns } from '@ant-design/pro-table';
import ProTable from '@ant-design/pro-table';
import ProCard from '@ant-design/pro-card';
// @ts-ignore
import styles from './split.less';
import { request } from 'umi';
import { PageContainer } from '@ant-design/pro-layout';

type TableListItem = {
  createdAtRange?: number[];
  createdAt: number;
  code: string;
};

type DetailListProps = {
  typeCode: string;
};

const DictionaryList: React.FC<DetailListProps> = (props) => {
  const { typeCode } = props;
  const [tableListDataSource, setTableListDataSource] = useState<TableListItem[]>([]);

  const columns: ProColumns<TableListItem>[] = [
    {
      title: '时间点',
      key: 'createdAt',
      dataIndex: 'createdAt',
      valueType: 'dateTime',
    },
    {
      title: '代码',
      key: 'code',
      width: 80,
      dataIndex: 'code',
      valueType: 'code',
    },
    {
      title: '操作',
      key: 'option',
      width: 80,
      valueType: 'option',
      render: () => [<a key="a">预警</a>],
    },
  ];

  useEffect(() => {
    const source = [];
    for (let i = 0; i < 15; i += 1) {
      source.push({
        createdAt: Date.now() - Math.floor(Math.random() * 10000),
        code: `const getData = async params => {};`,
        key: i,
      });
    }

    setTableListDataSource(source);
  }, [typeCode]);

  return (
    <ProTable<TableListItem>
      columns={columns}
      dataSource={tableListDataSource}
      pagination={{
        pageSize: 3,
        showSizeChanger: false,
      }}
      rowKey="key"
      toolBarRender={false}
      search={false}
    />
  );
};

type statusType = BadgeProps['status'];

const valueEnum: statusType[] = ['success', 'error', 'processing', 'default'];

export type IpListItem = {
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
