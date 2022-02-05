import React, { useRef, useState } from 'react';
import type { ProColumns } from '@ant-design/pro-table';
import { EditableProTable } from '@ant-design/pro-table';
import type { ActionType } from '@ant-design/pro-table';
import { Button, Input, Space, Tag, Form } from 'antd';
import styles from '../index.less'
import { CheckCircleOutlined, CopyOutlined, DeleteOutlined, PlusOutlined, ScissorOutlined } from '@ant-design/icons';
import { useModel } from 'umi';
import {useEffect} from 'react';

const waitTime = (time: number = 100, key: any, rows: any) => {
  console.log(key, rows, 'values', )
  return new Promise((resolve) => {
    setTimeout(() => {
      resolve(true);
    }, time);
  });
};


type DataSourceType = {
  id: React.Key;
  parameterName?: string;
  displayTitle?: string;
  parameterType?: string;
  displayOrder?: number;
};

// const defaultData: DataSourceType[] = [
//   {
//       "id": 1,
//       "parameterName": "p_nf",
//       "displayTitle": "统计年份",
//       "displayOrder": 1,
//       "parameterType": "数值型"
//   },
//   {
//       "id": 2,
//       "parameterName": "p_hy",
//       "displayTitle": "行业类型",
//       "displayOrder": 2,
//       "parameterType": "代码表"
//   }
// ]

const columns: ProColumns<DataSourceType>[] = [
  {
    key: '1',
    title: '参数名称',
    dataIndex: 'parameterName',
    formItemProps: {
      rules: [
        {
          required: true,
          message: '请输入参数名称',
        },
      ],
    },
    width: '22%',
  },
  {
    key: '2',
    title: '显示名称',
    dataIndex: 'displayTitle',
    width: '20%',
  },
  {
    key: '3',
    title: '参数类型',
    dataIndex: 'parameterType',
    valueType: 'select',
    width: '20%',
    valueEnum: {
      all: { text: '代码表', status: 'Default' },
      open: {
        text: '字符型',
        status: 'Error',
      },
      closed: {
        text: '已解决',
        status: 'Success',
      },
    },
  },
  {
    key: '4',
    title: '顺序',
    dataIndex: 'displayOrder',
    width: '20%',
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

const ParameterTable = () => {

  const { resultParameters, changeParameters } = useModel("guidelineModels", (ret) => ({
    resultParameters: ret.parameters,
    changeParameters: ret.changeParameters
  }))

  console.log(resultParameters, '-----参数---列表展示', changeParameters)

  // useEffect(()=> {
  //   console.log( '12345')
  // },[])

  // useEffect(() => {
  //   console.log(resultColumns, '----edffff----列表展示', changeColumns)
  // // eslint-disable-next-line react-hooks/exhaustive-deps
  // },[resultColumns])
  const actionRef = useRef<ActionType>();
  const [editableKeys, setEditableRowKeys] = useState<React.Key[]>([]);
  const [form] = Form.useForm();
  return (
    <>
      <Space>
        <Button
          type="primary"
          onClick={() => {
            actionRef.current?.addEditRecord?.({
              id: (Math.random() * 1000000).toFixed(0),
              title: '添加参数',
            });
          }}
          icon={<PlusOutlined />}
        >
          添加参数
        </Button>
        <Button
          key="rest"
          onClick={() => {
            form.resetFields();
          }}
        >
          重置表单
        </Button>
        {/* <Button type="dashed" icon={<DeleteOutlined />} className={styles.buttonmarginright}>删除参数</Button> */}
        <Button type="dashed" icon={<CopyOutlined />} className={styles.buttonmarginright}>复制参数</Button>
        <Button type='dashed' icon={<ScissorOutlined />} className={styles.buttonmarginright}>粘贴参数</Button>
        {/* <Button type="dashed" icon={<CheckCircleOutlined />} className={styles.buttonmarginright}>保存</Button> */}
        {/* <Button type='dashed' icon={<DeleteOutlined />}>取消</Button> */}
      </Space>

      <EditableProTable<DataSourceType>
        rowKey="id"
        actionRef={actionRef}
        maxLength={5}
        // 关闭默认的新建按钮
        recordCreatorProps={false}
        columns={columns}
        value={resultParameters}
        onChange={changeParameters}
        onRow={(row) => { return console.log(row);}}
        editable={{
          form,
          editableKeys,
          onSave: async (key,rows) => {
            await waitTime(2000,key, rows);
          },
          onChange: setEditableRowKeys,
          actionRender: (row, config, dom) => [dom.save, dom.cancel],
        }}
      />
    </>
  );
};

export default ParameterTable;
