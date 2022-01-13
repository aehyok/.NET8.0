import { Modal, Form,Input, Button, message, Select, Skeleton } from 'antd';
import { useRef, useEffect, useState } from 'react';
const { Option } = Select;
import ProForm, {
  ProFormText,
  ProFormDateRangePicker,
  ProFormTextArea,
  ProFormSelect
} from '@ant-design/pro-form';
import { getMenu } from '@/services/ant-design-pro/menu'

export default (props: any) => {

  const formItemLayout = {
    labelCol: { span: 4 },
    wrapperCol: { span: 20 },
  }

  const { modalVisible, hiddenModal, editId, actionRef } = props
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
      const result = await getMenu()
      console.log(result, 'result')
      setInitialValues(result.data)
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
    message.success('结果保存成功')
  }

  return (
    <Modal title="添加菜单" footer={null} visible={modalVisible} onOk={handleOk} onCancel={handleCancel}>
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
            name="pp"
            label="上级菜单"
            disabled
          />
          <ProFormText
            name="name"
            label="菜单名称"
            rules={[{ required: true, message: '请输入菜单名称' }]}
            placeholder="请输入菜单名称"
          />

          <ProFormText
            name="uiPath"
            label="菜单路由"
          />
          <ProFormText
            name="sequence"
            label="显示顺序"
            placeholder="请输入显示顺序"
          />
        <ProFormSelect
          options={[
            {
              value: '6',
              label: '禁用',
            },
            {
              value: '12',
              label: '启用',
            },
          ]}
          initialValue="6"
          width="xs"
          name="taxRate"
          label="是否禁用"
        />
        <ProFormTextArea label="参数" name="remark"/>
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
