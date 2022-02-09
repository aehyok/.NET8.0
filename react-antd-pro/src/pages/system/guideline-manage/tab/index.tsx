import React, { useEffect } from 'react';
import { Card, Tabs } from 'antd';
import ParameterTable from '../parameter-table';
import ResultTable from '../result-table';
const { TabPane } = Tabs;
const GuidelineTable = () => {
  return (
    <Card bordered={false} style={{marginTop: '15px'}}>
      <Tabs defaultActiveKey="1">
        <TabPane tab="参数定义" key="1">
          <ParameterTable />
        </TabPane>
        <TabPane tab="显示结果字段定义" key="2">
          <ResultTable />
        </TabPane>
      </Tabs>
    </Card>
  );
};

export default GuidelineTable;
