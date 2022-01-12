import { Modal, Form,Input, Button, message, Select, Skeleton } from 'antd';
import { useRef, useEffect, useState } from 'react';
const { Option } = Select;
import ProForm, {
  ProFormText,
  ProFormDateRangePicker,
  ProFormTextArea,
  ProFormSelect
} from '@ant-design/pro-form';
import { getUser } from '@/services/ant-design-pro/api'

export default (props: any) => {
  const { modalVisible, hiddenModal, editId, actionRef } = props
  console.log(props.modalVisible, editId, 'ssss----ss')
  console.log(actionRef, 'actionRef')
  const handleOk = () => {
    hiddenModal()
  }

  const [initialValues,setInitialValues] = useState(undefined)
  // eslint-disable-next-line react-hooks/exhaustive-deps
  useEffect(() => {
    const fetch = async () => {
      const result = await getUser()
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
  const [form] = Form.useForm();

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
          submitter={false}
          onFinish={async (values) => onSubmit(values)}
      >
        <ProForm.Group>
          <ProFormText
            name="name"
            label="菜单名称"
            placeholder="请输入菜单名称"
          />
          <ProFormText
            name="path"
            label="菜单路由"
          />
        </ProForm.Group>
        <ProForm.Group>
          <ProFormText
            name="displayOrder"
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
        </ProForm.Group>
        <ProFormTextArea width="xl" label="参数" name="remark"  autoSize ={{minRows: 6, maxRow: 10}} />
        <ProForm.Group style={{textAlign:'right'}}>
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
