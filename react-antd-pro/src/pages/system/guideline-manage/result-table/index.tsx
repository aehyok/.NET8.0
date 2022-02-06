import React, { useEffect, useRef, useState } from 'react';
import type { ActionType, ProColumns } from '@ant-design/pro-table';
import { EditableProTable } from '@ant-design/pro-table';
import { Button, Form, Space } from 'antd';
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

// const defaultData: DataSourceType[] = new Array(5).fill(1).map((_, index) => {
//   return {
//     id: (Date.now() + index).toString(),
//     title: `活动名称${index}`,
//     decs: '这个活动真好玩',
//     state: 'open',
//     created_at: '2020-05-26T09:42:56Z',
//   };
// });

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
  }

  console.log(resultColumns, '-----字段列表---列表展示', changeColumns)

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
        <EditableProTable.RecordCreator
          key="copy"
          record={{
            ...record,
            id: (Math.random() * 1000000).toFixed(0),
          }}
        >
          <a>删除</a>
        </EditableProTable.RecordCreator>,
      ],
    },
  ];


  return (
    <>
      <Space>
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
