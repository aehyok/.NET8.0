import React from 'react';
import { PageContainer } from '@ant-design/pro-layout';
import ProCard from '@ant-design/pro-card';
import FormList from './form-list'
import FromConfig from './form-config'
import { useModel } from 'umi';
const FormManage: React.FC = () => {
  const { setEditId} = useModel('formModels', (ret)=>({
    setEditId: ret.setEditId,
  }))

  setEditId('')
  return <>
    <PageContainer>
      <ProCard split="vertical">
        <ProCard colSpan="324px" ghost>
          <FormList />
        </ProCard>
        <ProCard>
          <FromConfig />
        </ProCard>
      </ProCard>
    </PageContainer>
  </>;
};

export default FormManage;

