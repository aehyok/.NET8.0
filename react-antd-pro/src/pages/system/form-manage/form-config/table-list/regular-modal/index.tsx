import { Modal, Form, Button, message, Space } from 'antd';
import { useRef, useState } from 'react';
import { ActionType, EditableProTable, ProColumns } from '@ant-design/pro-table';
import { PlusOutlined } from '@ant-design/icons';
import { useModel } from 'umi';
// eslint-disable-next-line @typescript-eslint/ban-types
const RulesModel = (props: {modalVisible: boolean, hiddenModal: Function, refresh: Function, currentRecord: any}) => {
  const { modalVisible, hiddenModal, refresh, currentRecord } = props
  console.log(props.modalVisible, modalVisible, 'ssss----ss')

  const { columnsList,setColumnsList} = useModel('formModels', (ret: any)=>({
    setColumnsList: ret.changeColumns,
    columnsList: ret.columns
  }))


  const [rulesList, setRulesList] = useState<RuleModel[]>(currentRecord.rules || [])
  const actionRef = useRef<ActionType>();

  const [form] = Form.useForm();

  const handleCancel = () => {
    form.resetFields()
    hiddenModal()
  }

  const handleOk = () => {

  }

  const onSaveClick = (rows: any) => {
    console.log(rulesList, rows, 'onSaveClick')
    const array: any = [...rulesList]
    const current: number = array.findIndex((item: any) => item.id === rows.id)
    console.log(current, current, 'current');

    if(current > -1 ) {
      array[current] = rows
      console.log(array, 'array-----update')
      setRulesList([...array])
    } else {
      array.push(rows)
      console.log(array, rows,'rules=-clist')
      setRulesList(array)
    }

    let formColumnList = [...columnsList]
    const currentColumn: number = formColumnList.findIndex((item: any) => item.id === currentRecord.id)
    console.log(currentColumn,formColumnList, currentRecord,'columns-rules=-clist')
    formColumnList[currentColumn] = {
      ...currentRecord,
      rules: [
        ...array
      ]
    }
    console.log(formColumnList,'columns-formColumnList-clist')
    message.warn('暂存正则后记的保存')
    setColumnsList(formColumnList)
  }

  type RuleModel = {
    id: string,
    message?: string,
    pattern?: string
  }

  const [editableKeys, setEditableRowKeys] = useState<React.Key[]>([]);
  
  const removeClick = (id: any) => {
    const array = rulesList.filter((item: any) => item.id !== id)
    console.log(array,'--array--')
    setRulesList(array)
    message.warn('移除后记的保存')
  }

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
                removeClick(record.id);
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
            <EditableProTable
                rowKey="id"
                actionRef={actionRef}
                maxLength={5}
                // 关闭默认的新建按钮
                recordCreatorProps={false}
                columns={columns}
                value={rulesList}
                onChange={setRulesList}
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
