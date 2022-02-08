import { FileAddOutlined } from '@ant-design/icons';
import TabPane from '@ant-design/pro-card/lib/components/TabPane';
import { ProFormDigit, ProFormText, ProFormTextArea } from '@ant-design/pro-form';
import { Button, Card, Col, Form, Row, Tabs } from 'antd';
import React, { useRef } from 'react';
import ColumnList from './column-list'
const FormConfig = () => {
  const formRef = useRef(null);
  const [form] = Form.useForm();

  const saveFormClick = () => {

  }
  return <>
        <Row justify={'space-between'}>
          <Col>
          </Col>
          <Col>
          <Button icon={<FileAddOutlined />} type="primary" onClick={() => saveFormClick()}>保存</Button>
          </Col>
        </Row>
        <Form
          hideRequiredMark
          form={form}
          ref={formRef}
          style={{ margin: 'auto', marginTop: 8, maxWidth: 600 }}
          name="basic"
          layout="horizontal"
          labelCol={{ span: 4 }}
          wrapperCol={{span: 20}}
          // onValuesChange ={(e: any) => { onChangeText(e)}}
        >
          <ProFormText
            width="md"
            label="表单名称"
            name="guideLineName"
            rules={[
              {
                required: true,
                message: '请输入表单名称',
              },
            ]}
            placeholder="请输入表单名称"
          />
          <ProFormDigit
            width="md"
            label="显示顺序"
            name="displayOrder"
            placeholder="请输入显示顺序"
          />
          <ProFormTextArea
            label="备注"
            name="remark"
            // autoSize={{minRows: 3, maxRows: 6}}
            placeholder="请输入备注"
          />
        </Form>
        <Card bordered={false} style={{marginTop: '15px'}}>
          <Tabs defaultActiveKey="1">
            <TabPane tab="表单字段列定义" key="1">
            <ColumnList />
            </TabPane>
          </Tabs>
        </Card>

  </>;
};

export default FormConfig;
