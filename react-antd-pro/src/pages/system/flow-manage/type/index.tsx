import { Form, Card, Tabs } from 'antd';
import {
  ProFormDigit,
  ProFormText,
  ProFormTextArea,
} from '@ant-design/pro-form';
import type { FC } from 'react';
import { useRef } from 'react';

const { TabPane } = Tabs;

const FlowType: FC<Record<string, any>> = (props: any) => {
  console.log(props, 'props')
  const formRef = useRef(null);
  const [form] = Form.useForm();

  const onFinish = async (values: Record<string, any>) => {
    console.log(values);
  };

  return (
    <Card bordered={false}>
      <Tabs defaultActiveKey="1">
        <TabPane tab="流程画布" key="1">
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
              label="流程ID"
              name="id"
              disabled={true}
            />
            <ProFormText
              width="md"
              label="流程名称"
              name="name"
              rules={[
                {
                  required: true,
                  message: '请输入流程名称',
                },
              ]}
              placeholder="请输入流程名称"
            />
            <ProFormDigit
              width="md"
              label="显示顺序"
              name="displayOrder"
              placeholder="请输入显示顺序"
            />
            <ProFormTextArea
              label="流程描述"
              name="goal"
              // autoSize={{minRows: 3, maxRows: 6}}
              placeholder="流程描述"
            />
          </Form>
        </TabPane>
        </Tabs>
      </Card>
  );
};

export default FlowType;
