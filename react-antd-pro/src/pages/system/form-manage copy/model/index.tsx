import { MoreOutlined } from '@ant-design/icons';
import ProCard from '@ant-design/pro-card';
import { Button, Col, Form, Input, InputNumber, Row, Select } from 'antd';
import React from 'react'


type ModelProps = {
};
const FormModel: React.FC<ModelProps> = (props) => {
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
    <ProCard title={'模型定义'}>
      <Form
        {...layout}
      >
        <Row gutter={0} style={{width: '100%'}}>
          <Col span={12}>
            <Form.Item label="表单名称" name="username" rules={[{ required: true }]}>
              <Input />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item label="显示名称" name="password" rules={[{ required: true }]}>
              <Input />
          </Form.Item>
          </Col>
        </Row>
        <Row gutter={0} style={{width: '100%'}}>
          <Col span={12}>
            <Form.Item label="录入模型类型" name="username" rules={[{ required: true }]}>
              <Select placeholder="select your gender">
                <Option value="male">Male</Option>
                <Option value="female">Female</Option>
                <Option value="other">Other</Option>
              </Select>
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item label="参数类型" name="password" rules={[{ required: true }]}>
              <Select placeholder="select your gender">
                <Option value="male">Male</Option>
                <Option value="female">Female</Option>
                <Option value="other">Other</Option>
              </Select>
          </Form.Item>
          </Col>
        </Row>
        <Row gutter={0} style={{width: '100%'}}>
          <Col span={12}>
            <Form.Item label="单位代码" name="username" rules={[{ required: true }]}>
              <Input />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item label="显示顺序" name="sx" >
              <InputNumber style={{ width: '100%' }}/>
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
        <Row gutter={0} style={{width: '100%'}}>
          <Col span={24} >
            <Form.Item {...singleLayout} label="记录删除规则" name="sx" >
                <Input.TextArea style={{height: '160px'}} />
            </Form.Item>
          </Col>
        </Row>
        <Row gutter={0} style={{width: '100%'}}>
          <Col span={12}>
            <Form.Item label="取数据算法" name="username" >
              <Input />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item label="初始化算法" name="password" >
              <Input.Group compact style={{width: '100%'}}>
                <Input style={{ width: 'calc(100% - 40px)' }} defaultValue="" />
                <Button  icon={<MoreOutlined />} />
              </Input.Group>
          </Form.Item>
          </Col>
        </Row>
        <Row gutter={0} style={{width: '100%'}}>
          <Col span={12}>
            <Form.Item label="取新记录算法" name="username" >
              <Input />
            </Form.Item>
          </Col>
          <Col span={12} />
        </Row>
        <Row gutter={0} style={{width: '100%'}}>
          <Col span={12}>
            <Form.Item label="资源类型" name="username" >
              <Input />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item label="集成应用" name="password" >
              <Input />
          </Form.Item>
          </Col>
        </Row>
        <Row gutter={0} style={{width: '100%'}}>
          <Col span={12}>
            <Form.Item label="写入前操作" name="username">
              <Input />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item label="写入后操作" name="password" >
              <Input />
          </Form.Item>
          </Col>
        </Row>
      </Form>
    </ProCard>
  )
}

export default FormModel



