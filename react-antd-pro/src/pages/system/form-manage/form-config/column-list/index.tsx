import { PlusOutlined } from '@ant-design/icons';
import ProCard from '@ant-design/pro-card';
import ProTable, { ActionType, EditableProTable, ProColumns } from '@ant-design/pro-table';
import { Button, Form, Space } from 'antd';
import React, { useRef, useState } from 'react';

type DataSource = {
  id?: string,
  name?: string,
  code?: string
}
const ColumnList = () => {
  const [editId, setEditId] = React.useState('')
  console.log('1111')
  const [columnsList, setColumnsList] = React.useState([
    {
      id: '1',
      name: 'code',
      title: '代码',
    },
    {
      id: '2',
      name: 'title',
      title: '标题',
    }
  ])

  const removeColumnsClick =(id: any) => {
    console.log(id, 'delete')
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
      width: 160,
      render: (text, record, _, action) => [
        <a
          key="editable"
          onClick={() => {
            action?.startEditable?.(record.id);
          }}
        >
          编辑
        </a>,
        <a
        key="regular"
        onClick={() => {removeColumnsClick(record.id)}}
      >
        正则
      </a>,
        <a
          key="delete"
          onClick={() => {removeColumnsClick(record.id)}}
        >
          移除
        </a>,
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
    <EditableProTable<DataSource>
        rowKey="id"
        actionRef={actionRef}
        maxLength={5}
        // 关闭默认的新建按钮
        recordCreatorProps={false}
        columns={columns}
        value={columnsList}
        // onChange={changeParameters}
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
