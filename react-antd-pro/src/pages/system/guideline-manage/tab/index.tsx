import React, { useEffect } from 'react';
import { Card, Tabs } from 'antd';
import ParameterTable from '../parameter-table';
const { TabPane } = Tabs;
const GuidelineTable = (props: any) => {
  const { guidelineData } = props



  useEffect(()=> {
    console.log('table-列表',guidelineData)

  },[guidelineData])

  return (
    <Card bordered={false} style={{marginTop: '15px'}}>
      <Tabs defaultActiveKey="1">
        <TabPane tab="参数定义" key="1">
          <ParameterTable />
        </TabPane>
        <TabPane tab="显示结果字段定义" key="2">
          Content of Tab Pane 2
        </TabPane>
      </Tabs>
    </Card>
  );
};

export default GuidelineTable;
