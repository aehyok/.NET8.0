import { Modal, Form,Input, Button, message, Select, Skeleton } from 'antd';
import { useRef, useEffect, useState } from 'react';
const { Option } = Select;
import ProForm, {
  ProFormText,
  ProFormDateRangePicker,
  ProFormTextArea,
  ProFormSelect
} from '@ant-design/pro-form';
import { getDictionaryType, addDictionaryType, updateDictionaryType } from '@/services/ant-design-pro/dictionary'
import { ActionType } from '@ant-design/pro-table';
import type { SYSTEM } from '@/services/ant-design-pro/typings'

type ModalProps = {
  modalVisible: boolean;
  hiddenModal: Function;
  editId: string;
  actionRef: ActionType;
  fatherId: string;
};

const TypeModal: React.FC<ModalProps> = (props) => {

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
      // SYSTEM.DictionaryTypeItem
       const result = await getDictionaryType<SYSTEM.DictionaryTypeItem>(editId)
        console.log(result, 'result----type')
        setInitialValues(result?.data)
      // initialValues = result.data
    }
    if(editId !== undefined) {
      fetch()
    }

    console.log('---fetch---')
  }, [])

  const handleCancel = () => {
    form.resetFields()
    hiddenModal()
  }

  const onSubmit = async(values: any) => {
    console.log(values, '保存结果')
    console.log(editId != undefined, 'editId')
    if(editId != '') {
      updateDictionaryType({
        ...values,
        id: editId,
      }).then((result: any) => {
        if(result.code == 200) {
          console.log(result, 'result')
          handleCancel()
          actionRef?.current?.reload()
          message.success('修改菜单成功')
        }
      })
    } else {
      addDictionaryType({
        ...values,
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
    <Modal title="添加字典类型" footer={null} visible={modalVisible} onOk={handleOk} onCancel={handleCancel}>
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
            name="name"
            label="类型名称"
            rules={[{ required: true, message: '请输入字典类型名称' }]}
            placeholder="请输入字典类型名称"
          />

          <ProFormText
            name="code"
            label="类型代码"
            rules={[{ required: true, message: '请输入字典类型代码' }]}
            placeholder="请输入字典类型代码"
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

export default TypeModal;
