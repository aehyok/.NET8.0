import React from 'react';
import { Button } from 'antd';
import { TableDropdown } from '@ant-design/pro-table';
import ProTable from '@ant-design/pro-table';
// @ts-ignore
import styles from './split.less';
import { request } from 'umi';

type TypeProps = {
  typeCode: number | undefined;
  onChangeClick: (record: SYSTEM.DictionaryTypeItem) => void;
};

const DictionaryTypeList: React.FC<TypeProps> = (props) => {
  const { onChangeClick, typeCode } = props;

  const columns: ProColumns<SYSTEM.DictionaryTypeItem>[] = [
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
        <TableDropdown
          key="actionGroup"
          menus={[
            { key: 'copy', name: '编辑' },
            { key: 'delete', name: '删除' },
          ]}
        />,
      ],
    },
  ];
  return (
    <ProTable<SYSTEM.DictionaryTypeItem>
      columns={columns}
      showHeader={false}
      request={(params, sorter, filter) => {
        // 表单搜索项会从 params 传入，传递给后端接口。
        console.log(params, sorter, filter);
        return request<SYSTEM.DictionaryTypeList>('/api/getDictionaryTypeList');
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

            onChangeClick(record)
          },
        };
      }}
    />
  );
};

export default DictionaryTypeList
