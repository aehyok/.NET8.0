import { message, Form } from 'antd';
import {
  ProFormDigit,
  ProFormText,
  ProFormTextArea,
} from '@ant-design/pro-form';
import { useRequest } from 'umi';
import type { FC } from 'react';
import { useRef, useEffect } from 'react';
import { fakeSubmitForm } from './service';

const GuidelineForm: FC<Record<string, any>> = (props: any) => {
  const { guidelineData } = props

  const formRef = useRef(null);
  const [form] = Form.useForm();

  useEffect(() => {
    console.log('form-表单', guidelineData)
    form.setFieldsValue({
      name: guidelineData.guideLineName,
      id: guidelineData.id,
      displayOrder:guidelineData.displayOrder
    });
  // eslint-disable-next-line react-hooks/exhaustive-deps
  },[guidelineData]);

  const { run } = useRequest(fakeSubmitForm, {
    manual: true,
    onSuccess: () => {
      message.success('提交成功');
    },
  });


  const onFinish = async (values: Record<string, any>) => {
    run(values);
  };

  return (
        <Form
          hideRequiredMark
          form={form}
          ref={formRef}
          style={{ margin: 'auto', marginTop: 8, maxWidth: 600 }}
          name="basic"
          layout="horizontal"
          labelCol={{ span: 4 }}
          wrapperCol={{span: 20}}
          initialValues={{ public: '1' }}
          onFinish={onFinish}
        >
          <ProFormText
            width="md"
            label="指标名称"
            name="name"
            rules={[
              {
                required: true,
                message: '请输入指标名称',
              },
            ]}
            placeholder="给目标起个名字"
          />
          <ProFormText
            width="md"
            label="指标ID"
            name="id"
            rules={[
              {
                required: true,
                message: '请输入ID',
              },
            ]}
            placeholder="给目标起个名字"
          />
          <ProFormDigit
            width="md"
            label="显示顺序"
            name="displayOrder"
            rules={[
              {
                required: true,
                message: '请输入ID',
              },
            ]}
            placeholder="给目标起个名字"
          />
          <ProFormTextArea
            label="指标算法"
            width="xl"
            name="goal"
            rules={[
              {
                required: true,
                message: '请输入目标描述',
              },
            ]}
            placeholder="请输入你的阶段性工作目标"
          />
        </Form>
  );
};

export default GuidelineForm;
