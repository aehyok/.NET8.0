import { PageContainer } from '@ant-design/pro-layout'
import React, { useState } from 'react'
import { Button, Col, Row, Tree } from 'antd';
import { DownOutlined, ReconciliationOutlined, SmileOutlined } from '@ant-design/icons';
import FormModel from './model'
import FormColumn from './column'
import FormTable from './table'
const FormManage = () => {
  // 选择模型中的节点类型
  const [type, setType] = useState(undefined)
  const onSelect = (selectedKeys: any, info: any) => {
    console.log('selected', selectedKeys, info);
    setType(info.node.type)
  };

  const treeData =[
    {
      title: '疫情防控',
      key: '0-0',
      type: 1,
      icon: <SmileOutlined />,
      children: [
        {
          title: '录入界面字段定义',
          key: '0-0-1',
          icon: <ReconciliationOutlined />,
          children: [
            {
              type: 2,
              title: 'leaf',
              key: '0-0-0-0',
            },
            {
              type: 2,
              title: 'leaf',
              key: '0-0-0-1',
            },
            {
              type: 2,
              title: 'leaf',
              key: '0-0-0-2',
            },
          ],
        },
        {
          title: '写入表字段定义',
          key: '0-0-2',
          type: 3,
          icon: <ReconciliationOutlined />,
          children: [
            {
              title: 'leaf',
              key: '0-0-1-0',
            },
          ],
        },
        {
          title: '子记录录入模型',
          key: '0-0-3',
          type: 4,
          icon: <ReconciliationOutlined />,
          children: [
            {
              title: 'leaf',
              key: '0-0-2-0',
            },
            {
              title: 'leaf',
              key: '0-0-2-1',
            },
          ],
        },
      ],
    },
    {
      title: '随手拍',
      key: '0-1',
      type: 1,
      icon: <SmileOutlined />,
    },
    {
      title: '户籍人口',
      key: '0-2',
      type: 1,
      icon: <SmileOutlined />,
    }
  ]

  return (
      <PageContainer>
      <Row gutter={12}>
          <Col lg={7} md={24}>
            <Row style={{margin: '5px'}}>
              <Col>
                <Button>添加</Button>
                <Button>删除</Button>
                <Button>导入</Button>
                <Button>导出</Button>
              </Col>
            </Row>
            <Tree
              showIcon
              // showLine
              // switcherIcon={<DownOutlined />}
              defaultExpandedKeys={['0-0-0']}
              onSelect={onSelect}
              treeData={treeData}
            />
          </Col>
          <Col lg={17} md={24}>
            {
               type === 1 ? <FormModel /> :
               type === 2 ? <FormColumn /> :
               type === 3 ? <FormTable /> : ''
            }
          </Col>
        </Row>

      </PageContainer>
  )
}

export default FormManage
