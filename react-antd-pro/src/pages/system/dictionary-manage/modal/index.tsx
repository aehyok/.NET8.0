import { Modal, Form,Input, Button, message, Select, Skeleton } from 'antd';
import { useRef, useEffect, useState } from 'react';
const { Option } = Select;
import ProForm, {
  ProFormText,
  ProFormDateRangePicker,
  ProFormTextArea,
  ProFormSelect
} from '@ant-design/pro-form';
import { getMenu, addMenu, updateMenu } from '@/services/ant-design-pro/menu'
import { ActionType } from '@ant-design/pro-table';

type ModalProps = {
  modalVisible: boolean;
  hiddenModal: Function;
  editId?: string;
  actionRef?: ActionType;
  fatherId: string;
};

const MenuModal: React.FC<ModalProps> = (props) => {
// export default (props: any) => {

  const formItemLayout = {
    labelCol: { span: 4 },
    wrapperCol: { span: 20 },
  }

  const { modalVisible, hiddenModal, editId, actionRef, fatherId } = props
  console.log(props.modalVisible, editId, 'ssss----ss')
  console.log(actionRef, 'actionRef')
  const [form] = Form.useForm();

  const handleOk = () => {
    hiddenModal()
  }

  const [initialValues,setInitialValues] = useState<SYSTEM.MenuItem>()
  // eslint-disable-next-line react-hooks/exhaustive-deps
  useEffect(() => {
    const fetch = async () => {
       await getMenu(editId).then((result: any) => {
        console.log(result, 'result')
        setInitialValues(result?.data)
      })
      // initialValues = result.data
    }
    if(editId !== undefined) {
      fetch()
    }

    console.log('---fetch---')
  }, [])

  const formRef = useRef(null);

  const handleCancel = () => {
    form.resetFields()
    hiddenModal()
  }

  const onSubmit = async(values: any) => {
    console.log(values, '保存结果')
    console.log(editId != undefined, 'editId')
    if(editId != undefined) {
      updateMenu({
        ...values,
        id: editId,
        fatherId: fatherId
      }).then((result: any) => {
        if(result.code == 200) {
          console.log(result, 'result')
          handleCancel()
          actionRef?.current?.reload()
          message.success('修改菜单成功')
        }
      })
    } else {
      addMenu({
        ...values,
        fatherId: fatherId,
      }).then((result: any) => {
        if(result.code == 200) {
          console.log(result, 'result')
          handleCancel()
          actionRef.current.reload()
          message.success('新增菜单成功')
        }
      })
    }
  }

  return (
    <Modal title="添加字典" footer={null} visible={modalVisible} onOk={handleOk} onCancel={handleCancel}>
      {
        initialValues === undefined && editId !== undefined ? <Skeleton /> :
        <ProForm
          form={form}
          {...formItemLayout}
          layout={'horizontal'}
          submitter={false}
          initialValues={initialValues}
          onFinish={async (values) => onSubmit(values)}
      >

          <ProFormText
            name="fatherCode"
            label="字典父节点代码"
            placeholder="请输入字典父节点代码"
          />
          <ProFormText
            name="name"
            label="字典名称"
            rules={[{ required: true, message: '请输入字典名称' }]}
            placeholder="请输入字典名称"
          />

          <ProFormText
            name="code"
            label="字典代码"
            rules={[{ required: true, message: '请输入字典代码' }]}
            placeholder="请输入字典代码"
          />
          <ProFormText
            name="displayOrder"
            label="显示顺序"
            placeholder="请输入显示顺序"
          />
        <ProFormTextArea label="备注" name="remark"/>
        <ProForm.Group style={{ textAlign: 'right' }}>
          <Button htmlType="button" onClick={ handleCancel }>
            取消
          </Button>
          <Button type="primary" htmlType="submit" style={{ marginRight :'20px'}} >
            保存
          </Button>
        </ProForm.Group>
      </ProForm>
    }
    </Modal>
  );
};

export default MenuModal;
