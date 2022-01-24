import { Modal, Form, Button, message, Select, Skeleton } from 'antd';
import { useRef, useEffect, useState } from 'react';
const { Option } = Select;
import ProForm, {
  ProFormText,
  ProFormTextArea,
  ProFormSelect,
  ProFormDigit
} from '@ant-design/pro-form';

import { getFlowEntityType,addFlowEntityType, updateFlowEntityType } from '@/services/ant-design-pro/flow'
export default (props: any) => {

  const formItemLayout = {
    labelCol: { span: 4 },
    wrapperCol: { span: 20 },
  }

  const { modalVisible, hiddenModal, editId, actionRef } = props

  const [form] = Form.useForm();

  const handleOk = () => {
    hiddenModal()
  }

  const [initialValues,setInitialValues] = useState<FLOW.FlowEntityType>()
  // eslint-disable-next-line react-hooks/exhaustive-deps
  useEffect(() => {
    const fetch = async () => {
      await getFlowEntityType(editId).then((result: any) => {
        console.log(result, 'result')
        if(result.code === 200) {
          setInitialValues(result?.data)
        }
      })
    }

    if(editId !== undefined) {
      fetch()
    } else {
      setInitialValues(undefined)
    }

    console.log('---fetch---')
  }, [])

  const handleCancel = () => {
    form.resetFields()
    hiddenModal()
  }

  const onSubmit = async(values: any) => {
    console.log(editId != undefined, 'editId')
    if(editId != undefined) {
      updateFlowEntityType({
        ...values,
        id: editId,
      }).then((result: any) => {
        if(result.code == 200) {
          console.log(result, 'result')
          handleCancel()
          actionRef.current.reload()
          message.success('修改流程成功')
        }
      })
    } else {
      addFlowEntityType({
        ...values
      }).then((result: any) => {
        if(result.code == 200) {
          console.log(result, 'result')
          handleCancel()
          actionRef.current.reload()
          message.success('新增流程成功')
        }
      })
    }

  }

  return (
    <Modal title="新增流程" footer={null} visible={modalVisible} onOk={handleOk} onCancel={handleCancel}>
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
            name="flowName"
            label="流程名称"
            rules={[{ required: true, message: '请输入流程名称' }]}
            placeholder="请输入流程名称"
          />
          <ProFormDigit
            name="displayOrder"
            label="显示顺序"
            placeholder="请输入显示顺序"
          />
          <ProFormSelect
            options={[
              {
                value: 0,
                label: '禁用',
              },
              {
                value: 1,
                label: '启用',
              },
            ]}
            width="xs"
            name="status"
            label="是否禁用"
          />
          <ProFormTextArea label="流程说明" name="description"/>
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
