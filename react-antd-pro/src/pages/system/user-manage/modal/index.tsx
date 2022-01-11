import { Modal, Form,Input, Button, message, Select } from 'antd';
import { useRef } from 'react';
import { InsertNewGuideLine } from '@/services/guideline/api'
const { Option } = Select;
// eslint-disable-next-line @typescript-eslint/ban-types
export default (props: {modalVisible: boolean, hiddenModal: Function}) => {
  const { modalVisible, hiddenModal } = props
  console.log(props.modalVisible, modalVisible, 'ssss----ss')

  const handleOk = () => {
    hiddenModal()
  }

  const layout = {
    labelCol: { span: 8 },
    wrapperCol: { span: 16 },
  };
  const formRef = useRef(null);
  const [form] = Form.useForm();

  const handleCancel = () => {
    form.resetFields()
    hiddenModal()
  }

  const onSubmit = async(values: any) => {
    const result = await InsertNewGuideLine({
      GuideLineName: values.GuideLineName,
      GroupName: 'ccc'
    })
    if(result.code === 200) {
      message.success('新增指标成功')
      handleCancel()
      // TODO 还没有刷新左侧树
    }
    console.log(result, '保存结果')
  }

  const onChange = () => {}

  const onSearch = () => {}
  return (
    <Modal title="添加用户" footer={null} visible={modalVisible} onOk={handleOk} onCancel={handleCancel}>
      <Form form={form} onFinish={(values: any)=> onSubmit(values) } ref={formRef} {...layout} >
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
          // rules={[{ required: true, message: '请输入账号' }]}
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
          name="role"
        >
        <Select
            showSearch
            placeholder="请选择用户角色"
            optionFilterProp="children"
            onChange={onChange}
            onSearch={onSearch}
          >
            <Option value="0">管理员</Option>
            <Option value="1">普通用户</Option>
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
    </Modal>
  );
};
