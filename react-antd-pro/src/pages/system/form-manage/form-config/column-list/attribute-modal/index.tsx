import { Modal, Form,Input, Button, message } from 'antd';
import { useRef } from 'react';
import { insertSystemForm } from '@/services/ant-design-pro/form'
import  { ColumnType } from '@/services/ant-design-pro/typings.d.ts'
import AttributeText from './attribute/text'
import AttributeNumber from './attribute/number'
import AttributeDate from './attribute/date'
import AttributeDaterange from './attribute/daterange'
// eslint-disable-next-line @typescript-eslint/ban-types

const AttributeModel = (props: {modalVisible: boolean, hiddenModal: Function, refresh: Function, currentRecord: any}) => {
  const { modalVisible, hiddenModal, refresh, currentRecord } = props
  console.log(props.modalVisible, modalVisible, 'ssss----ss')

  console.log(currentRecord, 'currentRecord')

  const handleOk = () => {
    hiddenModal()
  }
  const columnType = ColumnType[currentRecord.type]
  const ReturnComponent = () => {
      console.log(currentRecord.type, 'return component')
     switch(currentRecord.type) {
         case 'text': 
            return <AttributeText />;
         case 'number': 
            return <AttributeNumber />;
         case 'date': 
            return <AttributeDate />;
         case 'daterange': 
            return <AttributeDaterange />;
         default:
             break;   
     }
}

  const formRef = useRef(null);
  const [form] = Form.useForm();

  const handleCancel = () => {
    form.resetFields()
    hiddenModal()
  }

  const onSubmit = async(values: any) => {
    const result = await insertSystemForm({
      formName: values.formName,
    })
    if(result.code === 200) {
      message.success('新增表单成功')
      handleCancel()
      refresh()
      // TODO 还没有刷新左侧树
    }
    console.log(result, '保存结果')
  }
 
  return (
    <Modal title={currentRecord.title+ '-' + columnType} footer={null} visible={modalVisible} onOk={handleOk} onCancel={handleCancel}>
      <Form form={form} onFinish={(values: any)=> onSubmit(values) } ref={formRef}>
        {/* <Form.Item
          label="表单名称"
          name="formName"
          rules={[{ required: true, message: '请输入表单名称' }]}
        >
          <Input />
        </Form.Item> */}
        {ReturnComponent()}
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

export default AttributeModel

