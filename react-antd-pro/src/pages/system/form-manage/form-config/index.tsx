import { FileAddOutlined } from '@ant-design/icons';
import TabPane from '@ant-design/pro-card/lib/components/TabPane';
import { ProFormDigit, ProFormText, ProFormTextArea } from '@ant-design/pro-form';
import { Button, Card, Col, Form, message, Row, Tabs } from 'antd';
import React, { useEffect, useRef } from 'react';
import ColumnList from './column-list'
import TableList from './table-list'
import { useModel } from 'umi';
import { getSystemForm, updateSystemForm } from '@/services/ant-design-pro/form'
const FormConfig = () => {
  const formRef = useRef(null);
  const [form] = Form.useForm();

  const { editId,model, changeModel,changeColumns,columns} = useModel('formModels', (ret)=>({
    editId: ret.editId,
    model: ret.model,
    changeModel: ret.changeModel,
    changeColumns: ret.changeColumns,
    columns: ret.columns
  }))

  useEffect(()=> {
    if(editId) {
      getSystemForm(editId).then((result)=> {
        console.log(result, 'result----')
        form.setFieldsValue({
          formName: result.data.formName,
          id: result.data.id,
          displayOrder:result.data.displayOrder,
          remark: result.data.remark
        });
        changeModel(result.data)
        if(result.data.columnList && result.data.columnList.length> 0) {
          changeColumns( JSON.parse(result.data.columnList) || [])
        } else {
          changeColumns([])
        }
      })
    } else {
      changeModel({})
      changeColumns([])
    }
  }, [editId])


  const saveFormClick = async() => {
    const result = await updateSystemForm({
      ...model,
      columnList: JSON.stringify(columns)
    })
    console.log(result.code,'result', result)
    if(result.code == 200) {
      message.success('保存指标成功')
    }
  }
  const onChangeText = (e: any) => {
    changeModel({
      ...model,
      ...e
    })
    console.log(e,'sssss')
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
          onValuesChange ={(e: any) => { onChangeText(e)}}
        >
        <ProFormText
            width="md"
            label="表单ID"
            name="id"
            disabled={true}
          />
          <ProFormText
            width="md"
            label="表单名称"
            name="formName"
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
            <TabPane tab="写入表定义" key="2">
            <TableList />
            </TabPane>
          </Tabs>
        </Card>

  </>;
};

export default FormConfig;
