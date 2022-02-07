import { Form, Card } from 'antd';
import {
  ProFormDigit,
  ProFormText,
  ProFormTextArea,
} from '@ant-design/pro-form';
import type { FC } from 'react';
import { useRef, useEffect } from 'react';
import {  useModel } from 'umi'
const GuidelineForm: FC<Record<string, any>> = (props: any) => {
  const { guidelineData } = props

  const formRef = useRef(null);
  const [form] = Form.useForm();

  const {} = useModel('guidelineModels',)

  const { models, changeModel } = useModel('guidelineModels', (ret) => ({
    changeModel: ret.changeModel,
    models: ret.model,
  }));

  useEffect(() => {
    console.log('form-表单', models)
    form.setFieldsValue({
      guideLineName: guidelineData.guideLineName,
      id: guidelineData.id,
      displayOrder:guidelineData.displayOrder,
      guideLineMethod: guidelineData.guideLineMethod
    });
  // eslint-disable-next-line react-hooks/exhaustive-deps
  },[guidelineData]);

  const onChangeText = (e: any) => {
    changeModel({
      ...guidelineData,
      ...e
    })
    console.log(e,'sssss')

  }
  return (
    <Card bordered={false}>
        <Form
          hideRequiredMark
          form={form}
          ref={formRef}
          style={{ margin: 'auto', marginTop: 8, maxWidth: 600 }}
          name="basic"
          layout="horizontal"
          labelCol={{ span: 4 }}
          wrapperCol={{span: 20}}
          onValuesChange ={(e: any) => { onChangeText(e)}}
        >
          <ProFormText
            width="md"
            label="指标ID"
            name="id"
            disabled={true}
          />
          <ProFormText
            width="md"
            label="指标名称"
            name="guideLineName"
            rules={[
              {
                required: true,
                message: '请输入指标名称',
              },
            ]}
            placeholder="请输入指标名称"
          />
          <ProFormDigit
            width="md"
            label="显示顺序"
            name="displayOrder"
            placeholder="请输入显示顺序"
          />
          <ProFormTextArea
            label="指标算法"
            name="guideLineMethod"
            // autoSize={{minRows: 3, maxRows: 6}}
            placeholder="请输入指标算法"
          />
        </Form>
      </Card>
  );
};

export default GuidelineForm;
