import React, { useEffect, useState } from 'react';
import type { ProColumns } from '@ant-design/pro-table';
import { EditableProTable } from '@ant-design/pro-table';
import { Button } from 'antd';
import { ProFormField } from '@ant-design/pro-form';

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
  const { resultGroups } = props
  const [editableKeys, setEditableRowKeys] = useState<React.Key[]>([]);
  const [dataSource, setDataSource] = useState<DataSourceType[]>(() => []);

  useEffect(() => {
    console.log('props数据传递',resultGroups)
    if(resultGroups && resultGroups.length > 0) {
      resultGroups[0].fields.forEach((item: { id: any; })=> {
        item.id = (Math.random() * 1000000).toFixed(0)
      })

      console.log('props数据传递--1',resultGroups)
      setDataSource(resultGroups[0].fields)
      const keys = resultGroups[0].fields.map((item: { id: any; })=> {
        return item.id
      })
      setEditableRowKeys(keys)
      console.log('props数据传递--2',resultGroups[0].fields)
    }
  },[])

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
    },
    {
      title: '显示宽度',
      dataIndex: 'displayWidth',
    },
    {
      title: '可否隐藏',
      dataIndex: 'canHide',
    },
    {
      title: '对齐方式',
      dataIndex: 'textAlign',
      valueType: 'select',
      valueEnum: {
        CENTER: 'CENTER',
        LEFT: 'LEFT',
        RIGHT: 'RIGHT'
      },
    },
    {
      title: '显示格式',
      dataIndex: 'displayFormat',
    },
    {
      title: '操作',
      valueType: 'option',
      width: 250,
      render: () => {
        return null;
      },
    },
  ];

  return (
    <>
      <EditableProTable<DataSourceType>
        columns={columns}
        rowKey="id"
        value={dataSource}
        onChange={setDataSource}
        recordCreatorProps={{
          newRecordType: 'dataSource',
          record: () => ({
            id: (Math.random() * 1000000).toFixed(0),
          }),
        }}
        editable={{
          type: 'multiple',
          editableKeys,
          actionRender: (row, config, defaultDoms) => {
            return [defaultDoms.delete];
          },
          onValuesChange: (record, recordList) => {
            setDataSource(recordList);
          },
          onChange: setEditableRowKeys,
        }}
      />
    </>
  );
};
