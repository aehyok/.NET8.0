import React, { useRef, useState } from 'react';
import type { ProColumns } from '@ant-design/pro-table';
import { EditableProTable } from '@ant-design/pro-table';
import type { ActionType } from '@ant-design/pro-table';
import { Button, Form, Space, message } from 'antd';
import {PlusOutlined } from '@ant-design/icons';
import { useModel } from 'umi';

type DataSourceType = {
  id: React.Key;
  parameterName?: string;
  displayTitle?: string;
  parameterType?: string;
  displayOrder?: number;
};

const ParameterTable = () => {
  const { resultParameters, changeParameters } = useModel("guidelineModels", (ret: { parameters: any; changeParameters: any; }) => ({
    resultParameters: ret.parameters,
    changeParameters: ret.changeParameters
  }))

  const removeParameterClick = (id: any) => {
    const array = resultParameters.filter((item: any) => item.id !== id)
    console.log(array,'--array--')
    changeParameters(array)
    message.warn('移除后记的保存')
  }

  const columns: ProColumns<DataSourceType>[] = [
    {
      key: 'parameterName',
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
      key: 'displayTitle',
      title: '显示名称',
      dataIndex: 'displayTitle',
      width: '20%',
    },
    {
      key: 'parameterType',
      title: '参数类型',
      dataIndex: 'parameterType',
      valueType: 'select',
      width: '20%',
      valueEnum: {
        all: { text: '代码表' },
        open: {
          text: '字符型',
        },
        closed: {
          text: '已解决',
        },
      },
    },
    {
      key: 'displayOrder',
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
        <a
          key="delete"
          onClick={() => {
            removeParameterClick(record.id)
          }}
        >
          移除
        </a>,
      ],
    },
  ];

  console.log(resultParameters, '-----参数---列表展示', changeParameters)

  const onSaveClick = (rows: any) => {
    console.log(rows, 'onSaveClick')
    const array: any = resultParameters
    const current: number = array.findIndex((item: DataSourceType) => item.id === rows.id)
    console.log(current, current, 'current');

    if(current > -1 ) {
      // current = rows
      array[current] = rows
      console.log(array, 'array-----update')
      changeParameters([...array])
    } else {
      changeParameters([...resultParameters, rows])
    }
    message.warn('暂存后记的保存')
    console.log(current, resultParameters, 'sssss')
  }

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
        {/* <Button
          key="rest"
          onClick={() => {
            form.resetFields();
          }}
        >
          重置表单
        </Button> */}
        {/* <Button type="dashed" icon={<DeleteOutlined />} className={styles.buttonmarginright}>删除参数</Button> */}
        {/* <Button type="dashed" icon={<CopyOutlined />} className={styles.buttonmarginright}>复制参数</Button>
        <Button type='dashed' icon={<ScissorOutlined />} className={styles.buttonmarginright}>粘贴参数</Button> */}
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
    </>
  );
};

export default ParameterTable;
