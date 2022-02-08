import React, { useState } from 'react';
import ProCard from '@ant-design/pro-card';
// @ts-ignore
import { PageContainer } from '@ant-design/pro-layout';
import DictionaryTypeList from './dictionaryTypeList';
import DictionaryList from './dictionaryList';


const DictionaryManage: React.FC = () => {
  const [typeCode, setTypeCode] = useState<string>()
  const [title, setTitle] = useState<string>()
  const onChangeClick = (record: SYSTEM.DictionaryTypeItem) => {
    console.log('onChangeClick', record)
    setTypeCode(record.code)
    setTitle(record.name)
  }
  return (
    <PageContainer>
      <ProCard split="vertical">
        <ProCard colSpan="324px" ghost>
          <DictionaryTypeList onChangeClick={onChangeClick} typeCode={typeCode} />
        </ProCard>
        <ProCard>
          <DictionaryList typeCode={typeCode} title={title} />
        </ProCard>
      </ProCard>
    </PageContainer>
  );
};

export default DictionaryManage;
