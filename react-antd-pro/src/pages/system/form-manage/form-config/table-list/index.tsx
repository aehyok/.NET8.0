import { PlusOutlined } from '@ant-design/icons';
import RegularModal from './regular-modal';
import AttributeModel from './attribute-modal';
import ProTable, {
  ActionType,
  EditableProTable,
  ProColumns,
  TableDropdown,
} from '@ant-design/pro-table';
import { Button, Col, Form, message, Row, Space } from 'antd';
import React, { useRef, useState } from 'react';
import { useModel } from 'umi';
import { ColumnType } from '@/services/ant-design-pro/typings.d.ts';
import ProCard from '@ant-design/pro-card';

type DataSource = {
  id?: string;
  name?: string;
  title?: string;
  type?: string;
  required?: boolean;
  placeholder?: string;
  displayOrder: number;
  ifshow: string;
};

const TableList = () => {
  const { columnsList, setColumnsList } = useModel('formModels', (ret: any) => ({
    setColumnsList: ret.changeColumns,
    columnsList: ret.columns,
  }));

  const [isShowRegularModal, setIsShowRegularModal] = useState(false);
  const [isShowAttributeModal, setIsShowAttributeModal] = useState(false);
  const [currentRecord, setCurrentRecord] = useState({});
  console.log(columnsList, 'columnsList0000000000000000000');
  const removeClick = (id: any) => {
    const array = columnsList.filter((item: any) => item.id !== id);
    console.log(array, '--array--');
    setColumnsList(array);
    message.warn('移除后记的保存');
  };

  const onSaveClick = (rows: any) => {
    console.log(rows, 'onSaveClick');
    const array: any = columnsList;
    const current: number = array.findIndex((item: any) => item.id === rows.id);
    console.log(current, current, 'current');

    if (current > -1) {
      // current = rows
      array[current] = rows;
      console.log(array, 'array-----update');
      setColumnsList([...array]);
    } else {
      console.log(columnsList, rows, 'rows-clist');
      setColumnsList([...columnsList, rows]);
    }
    console.log(current, columnsList, 'sssss');
    message.warn('暂存后记的保存');
  };

  const updateOtherClick = (record: any) => {
    console.log(record, 'record--==update');
    setIsShowAttributeModal(true);
    setCurrentRecord(record);
  };

  const regularClick = (record: any) => {
    console.log('regular');
    setIsShowRegularModal(true);
  };

  const operationClick = (type: string, record: any) => {
    setCurrentRecord(record);
    if (type === 'regular') {
      regularClick(record);
    }
    if (type === 'other') {
      if (record.type) {
        updateOtherClick(record);
      } else {
        message.warn('请先选择字段类型');
      }
    }
    if (type === 'remove') {
      removeClick(record.id);
    }
  };

  const columnList = {};
  for (const [key, value] of Object.entries(ColumnType)) {
    console.log(key, value, 'key-value');
    columnList[key] = value;
  }

  const columns: ProColumns<DataSource>[] = [
    {
      title: '写入表字段',
      dataIndex: 'name',
    },
    {
      title: '显示名称',
      dataIndex: 'title',
    },
    {
      title: '前端定义字段',
      dataIndex: 'type',
      valueEnum: columnList,
      // valueEnum: {
      //   static: '只读文本',
      //   text: '文本框',
      //   textarea: '文本域',
      //   select: '下拉列表',
      //   editor: '富文本',
      //   number: '数值框',
      //   image: '上传图片',
      //   video: '上传视频',
      //   date: '日期',
      //   daterange: '日期范围'
      // },
    },
    {
      title: '是否必填',
      dataIndex: 'required',
      // valueEnum: {
      //   true: '必填',
      //   false: '非必填',
      // },
      valueType: 'select',
      fieldProps: {
        options: [
          {
            label: '必填项',
            value: true,
          },
          {
            label: '非必填项',
            value: false,
          },
        ],
      },
    },
    {
      title: '显示条件',
      dataIndex: 'ifshow',
    },
    {
      title: '占位符',
      dataIndex: 'placeholder',
    },
    {
      title: '顺序',
      dataIndex: 'displayOrder',
      valueType: 'digit',
    },
    {
      title: '操作',
      valueType: 'option',
      width: 150,
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
          onSelect={(type: any) => {
            operationClick(type, record);
          }}
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

  return (
    <>
      {!isShowAttributeModal ? (
        ''
      ) : (
        <AttributeModel
          modalVisible={isShowAttributeModal}
          hiddenModal={setIsShowAttributeModal}
          refresh={setIsShowAttributeModal}
          currentRecord={currentRecord}
        />
      )}
      {!isShowRegularModal ? (
        ''
      ) : (
        <RegularModal
          modalVisible={isShowRegularModal}
          hiddenModal={setIsShowRegularModal}
          refresh={setIsShowRegularModal}
          currentRecord={currentRecord}
        />
      )}
      <ProCard split="vertical">
        <ProCard colSpan="224px" ghost>
        <Row>
          <Col>
            户籍人口表[VillagePopulation]
          </Col>
        </Row>
        <Row>
          <Col>
            户籍人口表12[VillagePopulation12]
          </Col>
        </Row>
        </ProCard>
        <ProCard>
          <Space style={{ marginLeft: '25px', marginBottom: '10px' }}>
          <Button
              type="primary"
              onClick={() => {
                actionRef.current?.addEditRecord?.({
                  id: (Math.random() * 1000000).toFixed(0),
                });
              }}
              icon={<PlusOutlined />}
            >
              添加数据表
            </Button>
            <Button
              type="primary"
              onClick={() => {
                actionRef.current?.addEditRecord?.({
                  id: (Math.random() * 1000000).toFixed(0),
                });
              }}
              icon={<PlusOutlined />}
            >
              同步数据表字段
            </Button>
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
            editable={{
              form,
              saveText: '暂存',
              editableKeys,
              onSave: async (key, rows) => {
                console.log('baocun', key, rows);
                onSaveClick(rows);
              },
              onChange: setEditableRowKeys,
              actionRender: (row, config, dom) => [dom.save, dom.cancel],
            }}
          />
        </ProCard>
      </ProCard>
    </>
  );
};

export default TableList;
