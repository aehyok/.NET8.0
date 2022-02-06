import { Modal, Form,Input, Button, message } from 'antd';
import { useRef } from 'react';
import { InsertNewGuideLine } from '@/services/guideline/api'

// eslint-disable-next-line @typescript-eslint/ban-types
const GuidelineModal = (props: {modalVisible: boolean, hiddenModal: Function, refresh: Function,selectGuidelineId: any}) => {
  const { modalVisible, hiddenModal, selectGuidelineId, refresh } = props
  console.log(props.modalVisible, modalVisible, 'ssss----ss')

  const handleOk = () => {
    hiddenModal()
  }

  const formRef = useRef(null);
  const [form] = Form.useForm();

  const handleCancel = () => {
    form.resetFields()
    hiddenModal()
  }

  const onSubmit = async(values: any) => {
    console.log('submit--abb', selectGuidelineId,values)
    const result = await InsertNewGuideLine({
      GuideLineName: values.GuideLineName,
      FatherID: selectGuidelineId[0],
    })
    if(result.code === 200) {
      message.success('新增指标成功')
      handleCancel()
      refresh()
      // TODO 还没有刷新左侧树
    }
    console.log(result, '保存结果')
  }
  return (
    <Modal title="添加指标" footer={null} visible={modalVisible} onOk={handleOk} onCancel={handleCancel}>
      <Form form={form} onFinish={(values: any)=> onSubmit(values) } ref={formRef}>
        <Form.Item
          label="指标名称"
          name="GuideLineName"
          rules={[{ required: true, message: '请输入指标名称' }]}
        >
          <Input />
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

export default GuidelineModal
