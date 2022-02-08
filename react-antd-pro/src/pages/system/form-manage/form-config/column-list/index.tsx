import { PlusOutlined } from '@ant-design/icons';
import ProCard from '@ant-design/pro-card';
import ProTable, { ActionType, EditableProTable, ProColumns, TableDropdown } from '@ant-design/pro-table';
import { Button, Form, message, Space } from 'antd';
import React, { useRef, useState } from 'react';
import { useModel } from 'umi';

type DataSource = {
  id?: string,
  name?: string,
  title?: string,
  type?: string,
  required?: boolean,
  placeholder?: string,

}

const ColumnList = () => {
  const { columnsList,setColumnsList} = useModel('formModels', (ret)=>({
    setColumnsList: ret.changeColumns,
    columnsList: ret.columns
  }))
  console.log(columnsList, 'columnsList0000000000000000000')
  const removeClick = (id: any) => {
    const array = columnsList.filter((item: any) => item.id !== id)
    console.log(array,'--array--')
    setColumnsList(array)
    message.warn('移除后记的保存')
  }

  const onSaveClick = (rows: any) => {
    console.log(rows, 'onSaveClick')
    const array: any = columnsList
    const current: number = array.findIndex((item: any) => item.id === rows.id)
    console.log(current, current, 'current');

    if(current > -1 ) {
      // current = rows
      array[current] = rows
      console.log(array, 'array-----update')
      setColumnsList([...array])
    } else {
      console.log(columnsList, rows,'rows-clist')
      setColumnsList([...columnsList, rows])
    }
    console.log(current, columnsList, 'sssss')
    message.warn('暂存后记的保存')
  }

  const updateOtherClick = (record: any) => {
    console.log(record, 'record')
  }

  const regularClick = (record: any) => {
    console.log('regular')
  }

  const operationClick = (type: string, record: any) => {
    if(type === 'regular') {
      regularClick(record)
    }
    if(type === 'other') {
      updateOtherClick(record)
    }
    if(type === 'remove') {
      removeClick(record.id)
    }
  }
  const columns: ProColumns<DataSource>[]= [
    {
      title: '接口字段名称',
      dataIndex: 'name',
    },
    {
      title: '显示名称',
      dataIndex: 'title',
    },
    {
      title: '字段类型',
      dataIndex: 'type',
      valueEnum: {
        static: '只读文本',
        text: '文本框',
        textarea: '文本域',
      },
    },
    {
      title: '是否必填',
      dataIndex: 'required',
      valueEnum: {
        true: '必填',
        false: '非必填',
      },
    },
    {
      title: '占位符提示',
      dataIndex: 'placeholder',
    },
    {
      title: '操作',
      valueType: 'option',
      width: 200,
      render: (text, record, _, action) => [
        <a
          key="editable"
          onClick={() => {
            action?.startEditable?.(record.id);
          }}
        >
          编辑
        </a>,
        <TableDropdown
        key="actionGroup"
        onSelect={(type: any) => { operationClick(type,record)}}
        menus={[
          { key: 'regular', name: '编辑正则' },
          { key: 'other', name: '编辑其他属性' },
          { key: 'remove', name: '移除' },
        ]}
      />,
      ],
    },
  ];

  const actionRef = useRef<ActionType>();
  const [editableKeys, setEditableRowKeys] = useState<React.Key[]>([]);
  const [form] = Form.useForm();

  return <>
    <Space style={{marginLeft: "25px", marginBottom:"10px"}}>
        <Button
          type="primary"
          onClick={() => {
            actionRef.current?.addEditRecord?.({
              id: (Math.random() * 1000000).toFixed(0),
            });
          }}
          icon={<PlusOutlined />}
        >
          添加字段
        </Button>
    </Space>
    <EditableProTable
        rowKey="id"
        actionRef={actionRef}
        maxLength={5}
        // 关闭默认的新建按钮
        recordCreatorProps={false}
        columns={columns}
        value={columnsList}
        onChange={setColumnsList}
        // onRow={(row) => { return console.log(row);}}
        editable={{
          form,
          saveText: '暂存',
          editableKeys,
          onSave: async (key,rows) => {
            console.log('baocun', key, rows)
            onSaveClick(rows)
          },
          onChange: setEditableRowKeys,
          actionRender: (row, config, dom) => [dom.save, dom.cancel],
        }}
      />
  </>;
};

export default ColumnList;
