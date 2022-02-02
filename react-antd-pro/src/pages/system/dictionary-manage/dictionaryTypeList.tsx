import React from 'react';
import { Button } from 'antd';
import { ProColumns, TableDropdown } from '@ant-design/pro-table';
import ProTable from '@ant-design/pro-table';
// @ts-ignore
import styles from './split.less';
import { request } from 'umi';

type TypeProps = {
  dictionaryTypeCode: string;
  onChangeClick: (record: SYSTEM.DictionaryTypeItem) => void;
};

const DictionaryTypeList: React.FC<TypeProps> = (props) => {
  const { onChangeClick, dictionaryTypeCode } = props;

  const addDictionaryType = () => {

  }
  const columns: ProColumns<SYSTEM.DictionaryTypeItem>[] = [
    {
      title: '字典类型',
      key: 'name',
      dataIndex: 'name',
    },
    {
      title: 'code',
      key: 'code',
      dataIndex: 'code',
      hideInTable: false,
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
      // showHeader={false}
      request={(params, sorter, filter) => {
        // 表单搜索项会从 params 传入，传递给后端接口。
        console.log(params, sorter, filter);
        return request<any>('/so/api/Dictionary/getDictionaryTypeList');
      }}
      rowKey="id"
      rowClassName={(record) => {
        return record.code === dictionaryTypeCode ? styles['split-row-select-active'] : '';
      }}
      toolbar={{
        search: {
          onSearch: (value) => {
            alert(value);
          },
        },
        actions: [
          <Button key="list" type="primary" onClick={() => addDictionaryType()}>
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
