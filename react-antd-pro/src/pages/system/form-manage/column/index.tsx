import { MoreOutlined } from '@ant-design/icons';
import ProCard from '@ant-design/pro-card';
import { Button, Col, Form, Input, InputNumber, Row, Select } from 'antd';
import React from 'react'


type ColumnProps = {
};
const FormColumn: React.FC<ColumnProps> = (props) => {
  console.log(props, 'props')

  const layout = {
    labelCol: { span: 6 },
    wrapperCol: { span: 18 },
  };

  const singleLayout = {
    labelCol: { span: 3 },
    wrapperCol: { span: 21 },
  }
  const { Option } = Select;
  return (
    <ProCard title={'字段分组定义'}>
      <Form
        {...layout}
      >
        <Row gutter={0} style={{width: '100%'}}>
          <Col span={12}>
            <Form.Item label="分组名称" name="username">
              <Input />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item label="显示顺序" name="password">
              <Input />
          </Form.Item>
          </Col>
        </Row>
        <Row gutter={0} style={{width: '100%'}}>
          <Col span={12}>
            <Form.Item label="分组类型" name="username">
              <Select placeholder="select your gender">
                <Option value="male">Male</Option>
                <Option value="female">Female</Option>
                <Option value="other">Other</Option>
              </Select>
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item label="应用注册URL" name="password">
              <Input />
          </Form.Item>
          </Col>
        </Row>

        <Row gutter={0} style={{width: '100%'}}>
          <Col span={24} >
            <Form.Item {...singleLayout} label="模型描述" name="sx" >
                <Input.TextArea  style={{height: '100px'}}/>
            </Form.Item>
          </Col>
        </Row>
      </Form>
    </ProCard>
  )
}

export default FormColumn



