import { PageContainer } from '@ant-design/pro-layout'
import React from 'react'
import { Col, Row, Tree } from 'antd';
import { DownOutlined, ReconciliationOutlined, SmileOutlined } from '@ant-design/icons';

const FormManage = () => {
  const onSelect = (selectedKeys: any, info: any) => {
    console.log('selected', selectedKeys, info);
  };

  const treeData =[
    {
      title: '疫情防控',
      key: '0-0',
      icon: <SmileOutlined />,
      children: [
        {
          title: '录入界面字段定义',
          key: '0-0-1',
          icon: <ReconciliationOutlined />,
          children: [
            {
              title: 'leaf',
              key: '0-0-0-0',
            },
            {
              title: 'leaf',
              key: '0-0-0-1',
            },
            {
              title: 'leaf',
              key: '0-0-0-2',
            },
          ],
        },
        {
          title: '写入表字段定义',
          key: '0-0-2',
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
      icon: <SmileOutlined />,
    },
    {
      title: '户籍人口',
      key: '0-2',
      icon: <SmileOutlined />,
    }
  ]

  return (
    <div>
      <PageContainer>
      <Row gutter={12}>
          <Col lg={7} md={24}>
            <div>
            <Tree
              showIcon
              // showLine
              // switcherIcon={<DownOutlined />}
              defaultExpandedKeys={['0-0-0']}
              onSelect={onSelect}
              treeData={treeData}
            />
            </div>
          </Col>
          <Col lg={17} md={24}>
            <Row gutter={24}>
              <Col span={24} >
                1
              </Col>
              <Col span={24}>
                2
              </Col>
            </Row>
          </Col>
        </Row>

      </PageContainer>
    </div>
  )
}

export default FormManage
