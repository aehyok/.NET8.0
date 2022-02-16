import { Modal, Form,Input, Button, message, Space } from 'antd';
import { useRef, useState } from 'react';
import { insertSystemForm } from '@/services/ant-design-pro/form'
import { ActionType, EditableProTable, ProColumns, TableDropdown } from '@ant-design/pro-table';
import { PlusOutlined } from '@ant-design/icons';

// eslint-disable-next-line @typescript-eslint/ban-types
const RulesModel = (props: {modalVisible: boolean, hiddenModal: Function, refresh: Function}) => {
  const { modalVisible, hiddenModal, refresh } = props
  console.log(props.modalVisible, modalVisible, 'ssss----ss')


  const [columnsList, setColumnsList] = useState<RuleModel[]>([])
  const actionRef = useRef<ActionType>();

  const [form] = Form.useForm();

  const handleCancel = () => {
    form.resetFields()
    hiddenModal()
  }

  const handleOk = () => {

  }

  const onSaveClick = () => {
    
  }

  type RuleModel = {
    id?: string,
    message?: string,
    pattern?: string
  }

  const [editableKeys, setEditableRowKeys] = useState<React.Key[]>([]);
  
  const columns: ProColumns<RuleModel>[]= [
    {
      title: '提示信息',
      dataIndex: 'message',
      width: 150,
    },
    {
      title: '正则表达式',
      dataIndex: 'pattern',
      width: 250,
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
        <a
            key="delete"
            onClick={() => {
                action?.startEditable?.(record.id);
            }}
            >
            删除
        </a>,
      ],
    },
  ];
  return (
      <>
        <Modal title="编辑正则" footer={null} visible={modalVisible} onOk={handleOk} onCancel={handleCancel} width={800}>
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
                添加正则
                </Button>
            </Space>
            <EditableProTable<RuleModel>
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
                onSave: async (key,rows) => {
                    console.log('baocun', key, rows)
                    onSaveClick(rows)
                },
                onChange: setEditableRowKeys,
                actionRender: (row, config, dom) => [dom.save, dom.cancel],
                }}
            />
    </Modal>
      </>
  );
};

export default RulesModel
