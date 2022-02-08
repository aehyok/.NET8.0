import React, { useEffect, useRef, useState } from 'react';
import type { ActionType, ProColumns } from '@ant-design/pro-table';
import { EditableProTable } from '@ant-design/pro-table';
import { Button, Form, message, Space } from 'antd';
import { ProFormField } from '@ant-design/pro-form';
import { useModel } from 'umi'
import { PlusOutlined } from '@ant-design/icons';

type DataSourceType = {
  id?: React.Key;
  fieldName?: string;
  displayTitle?: string;
  displayOrder?: number;
  displayWidth?: number;
  canHide: boolean;
  textAlign?: string;
  displayFormat: string;
};
export default (props: any) => {
  console.log(props, 'props')
  const [editableKeys, setEditableRowKeys] = useState<React.Key[]>([]);
  const actionRef = useRef<ActionType>();
  const [form] = Form.useForm();

  const { resultColumns, changeColumns } = useModel("guidelineModels", (ret) => ({
    resultColumns: ret.columns,
    changeColumns: ret.changeColumns
  }))

  const onSaveClick = (rows: any) => {
    console.log(rows, 'onSaveClick')
    const array: any = resultColumns
    const current: number = array.findIndex((item: DataSourceType) => item.id === rows.id)
    console.log(current, current, 'current');

    if(current > -1 ) {
      // current = rows
      array[current] = rows
      console.log(array, 'array-----update')
      changeColumns([...array])
    } else {
      changeColumns([...resultColumns, rows])
    }
    console.log(current, resultColumns, 'sssss')
    message.warn('暂存后记的保存')
  }

  console.log(resultColumns, '-----字段列表---列表展示', changeColumns)

  const removeColumnsClick = (id: any) => {
    const array = resultColumns.filter((item: any) => item.id !== id)
    console.log(array,'--array--')
    changeColumns(array)
    message.warn('移除后记的保存')
  }

  const columns: ProColumns<DataSourceType>[] = [
    {
      title: '名称',
      dataIndex: 'fieldName',
      width: '15%',
    },
    {
      title: '显示名称',
      dataIndex: 'displayTitle',
      width: '20%',
    },
    {
      title: '显示顺序',
      dataIndex: 'displayOrder',
      width: '20%',
    },
    {
      title: '显示宽度',
      dataIndex: 'displayWidth',
      width: '15%',
    },
    {
      title: '可否隐藏',
      dataIndex: 'canHide',
      width: '15%',
      valueEnum: {
        true: '可隐藏',
        false: '不隐藏',
      },
    },
    {
      title: '对齐方式',
      dataIndex: 'textAlign',
      valueType: 'select',
      width: '15%',
      valueEnum: {
        CENTER: 'CENTER',
        LEFT: 'LEFT',
        RIGHT: 'RIGHT'
      },
    },
    {
      title: '显示格式',
      dataIndex: 'displayFormat',
      width: '15%',
    },
    {
      title: '操作',
      valueType: 'option',
      width: 120,
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
          key="delete"
          onClick={() => {removeColumnsClick(record.id)}}
        >
          移除
        </a>,
      ],
    },
  ];


  return (
    <>
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
      <EditableProTable<DataSourceType>
        columns={columns}
        rowKey="id"
        value={resultColumns}
        // 关闭默认的新建按钮
        recordCreatorProps={false}
        actionRef={actionRef}
        onRow={(record) => {
          return {
            onClick: () => {
              console.log('recor--onrow--d', record)
            },
          };
        }}
        editable={{
          form,
          editableKeys,
          saveText: '暂存',
          onSave: async (key,rows) => {
            console.log('baocun', key, rows)
            onSaveClick(rows)
          },
          onChange: setEditableRowKeys,
          actionRender: (row, config, dom) => [dom.save, dom.cancel],
        }}
      />
    </>
  );
};
