import { Modal, Form,Input, Button, message, Select, Skeleton } from 'antd';
import { useRef, useEffect, useState } from 'react';
const { Option } = Select;
import { addUser, updateUser, getUser } from '@/services/ant-design-pro/user'

export default (props: any) => {
  const { modalVisible, hiddenModal, editId, actionRef } = props

  const handleOk = () => {
    hiddenModal()
  }

  const title = editId === undefined ? '添加用户': '编辑用户'
  const [initialValues,setInitialValues] = useState(undefined)
  // eslint-disable-next-line react-hooks/exhaustive-deps
  useEffect(() => {
    const fetch = async () => {
      console.log(editId, '--editId--')
      const result = await getUser({
        id:editId
      })
      console.log(result, 'result')
      setInitialValues(result.data)
    }
    if(editId !== undefined) {
      fetch()
    }

    console.log('---fetch---')
  }, [])

  const layout = {
    labelCol: { span: 6 },
    wrapperCol: { span: 18 },
  };
  const formRef = useRef(null);
  const [form] = Form.useForm();

  const handleCancel = () => {
    form.resetFields()
    hiddenModal()
  }

  const onSubmit = async(values: any) => {
    console.log(values, editId, '保存结果')
    let user = values
    if(editId !== undefined) {
      user= {
        ...values,
        id: editId
      }
      const result = await updateUser(user)
      if(result?.code === 200) {
        message.success('结果保存成功')
        actionRef.current.reload()
        hiddenModal()
        form.resetFields()
      }
      console.log(result, 'result-saveUser----')
    } else {
      const result = await addUser(user)
      if(result?.code === 200) {
        message.success('结果保存成功')
        actionRef.current.reload()
        hiddenModal()
        form.resetFields()
      }
      console.log(result, 'result-saveUser----')
    }

  }

  const onChange = () => {}

  const onSearch = () => {}
  return (
    <Modal title={title} footer={null} visible={modalVisible} onOk={handleOk} onCancel={handleCancel} destroyOnClose={true}>
      {
        initialValues === undefined && editId !== undefined ? <Skeleton /> :

      <Form form={form} onFinish={(values: any)=> onSubmit(values) } ref={formRef} {...layout}  initialValues={initialValues}>
        <Form.Item
          label="姓名"
          name="nickName"
          rules={[{ required: true, message: '请输入姓名' }]}
        >
          <Input />
        </Form.Item>
        <Form.Item
          label="账号"
          name="account"
          rules={[{ required: true, message: '请输入账号' }]}
        >
          <Input />
        </Form.Item>
        <Form.Item
          label="性别"
          name="sex"
        >
          <Select
            showSearch
            placeholder="请选择性别"
            optionFilterProp="children"
            onChange={onChange}
            onSearch={onSearch}
          >
            <Option value="0">未知</Option>
            <Option value="1">男</Option>
            <Option value="2">女</Option>
          </Select>
        </Form.Item>
        <Form.Item
          label="用户角色"
          name="roleInfo"
        >
        <Select
            showSearch
            placeholder="请选择用户角色"
            optionFilterProp="children"
            onChange={onChange}
            onSearch={onSearch}
          >
            <Option value="1">管理员</Option>
            <Option value="2">普通用户</Option>
          </Select>
        </Form.Item>
        <Form.Item style={{textAlign:'right'}}>
          <Button type="primary" htmlType="submit" style={{ marginRight :'20px'}} >
            保存
          </Button>
          <Button htmlType="button" onClick={ handleCancel }>
            取消
          </Button>
        </Form.Item>
      </Form>
    }
    </Modal>
  );
};
