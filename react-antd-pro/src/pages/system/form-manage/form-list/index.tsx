import ProTable, { ProColumns, TableDropdown } from '@ant-design/pro-table';
import { Button } from 'antd';
import React from 'react';
import { request } from 'umi';

const FormList = (props: any) => {
  console.log(props)

  const [editId, setEditId] = React.useState('')
  const addFormClick = () => {
    console.log('添加表单')
  }

  const deleteFormClick = (record: any) => {
    console.log('删除表单', record)
  }

  const columns: ProColumns<SYSTEM.FormItem>[] = [
    {
      title: '表单名称',
      key: 'formName',
      dataIndex: 'formName',
    },
    {
      title: '操作',
      valueType: 'option',
      render: (_: any,record: any) =>[
        <a key="delete" onClick={()=> {deleteFormClick(record)}}> 删除</a>
      ],
    },
  ];

  return <>
    <ProTable<SYSTEM.FormItem>
      rowKey="id"
      columns={columns}
      options={false}
      pagination={false}
      search={false}
      toolbar={{
        // search: {
        //   onSearch: (value) => {
        //     alert(value);
        //   },
        // },
        actions: [
          <Button key="list" type="primary" onClick={ () => { addFormClick()} }>
            新建
          </Button>,
        ],
      }}
      request={(params, sorter, filter) => {
        // 表单搜索项会从 params 传入，传递给后端接口。
        console.log(params, sorter, filter);
        return request<any>('/so/api/Form/GetSystemFormList');
      }}
      onRow={(record: any) => {
        return {
          onClick: () => {
            console.log('record', record)
            setEditId(record.id)
            // onChangeClick(record)
          },
        };
      }}
    />
  </>;
};

export default FormList;
