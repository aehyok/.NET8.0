import { Row, Col, Button, message, Modal } from 'antd';
import {PageContainer ,GridContent } from '@ant-design/pro-layout';
import React, { useEffect } from 'react';
import GuidelineForm from './form'
import GuidelineTable from './table'
import GuidelineModal from './modal'
import GuidelineTree from './tree'
import { GetGuidelineDefine , DelGuideLine} from '@/services/guideline/api'
import styles from './index.less'
import { CheckCircleOutlined, CopyOutlined, DeleteOutlined, ExclamationCircleOutlined, ExportOutlined, FileAddOutlined, ImportOutlined, ScissorOutlined } from '@ant-design/icons';

const GuidelineManage = () =>{

  const [isModalVisible, setIsModalVisible] = React.useState(false);

  const [selectGuidelineId, setSelectGuidelineId] = React.useState([])
  const [guidelineData, setGuidelineData] = React.useState({})

  const loadTreeList = async (id: any = "1") => {
    console.log(id, 'id')
    const response = await GetGuidelineDefine(id)
    console.log(response, '--detail--response--')
    if(response.data) {
      setGuidelineData(response.data)
    }
  }

  useEffect(()=> {
    console.log(selectGuidelineId, '12345')
    loadTreeList(selectGuidelineId)
  },[selectGuidelineId])

  const showModal = () => {
    if(selectGuidelineId && selectGuidelineId.length> 0) {
      setIsModalVisible(true);
    }
    else {
      message.warn(`请先选择指标父节点`);
    }
  };

  const deleteSubmitCallBack = async() => {
    if(selectGuidelineId && selectGuidelineId.length === 1) {
      const response = await DelGuideLine(selectGuidelineId[0])
      console.log(response, '--detail--response--')
      if(response.data) {
        setSelectGuidelineId([])
      }
    }
  }

  function confirm() {
    Modal.confirm({
      title: '系统提示',
      icon: <ExclamationCircleOutlined />,
      content: '请确认是否删除该指标（以及该指标节点下的指标）?',
      okText: '确认',
      onOk:() => {deleteSubmitCallBack()},
      onCancel : () => {console.log('取消')},
      cancelText: '取消',
    });
  }
  
  const deleteGuidelineClick = () => {
    confirm()
  }
  return (
    <PageContainer>
      <GuidelineModal modalVisible = {isModalVisible} hiddenModal = {setIsModalVisible} selectGuidelineId={selectGuidelineId} />
      <GridContent>
        <Row style={{margin: '5px 0'}} justify={'space-between'}>
          <Col> 
            <Button type="primary" icon={<FileAddOutlined />} onClick={() => showModal()} className={styles.buttonmarginright}>添加指标</Button>
            <Button type="dashed" icon={<DeleteOutlined />} onClick={() => deleteGuidelineClick()}>删除指标</Button>
            {/* <Button type="dashed" icon={ <ExportOutlined />}>导入指标</Button>
            <Button type="dashed" icon={ <ImportOutlined />}>导出指标</Button> */}
           </Col>
           <Col>
            <Button icon={<FileAddOutlined />} type="primary" className={styles.buttonmarginright}>添加参数</Button>
            <Button type="dashed" icon={<DeleteOutlined />} className={styles.buttonmarginright}>删除参数</Button>
            <Button type="dashed" icon={<CopyOutlined />} className={styles.buttonmarginright}>复制参数</Button>
            <Button type='dashed' icon={<ScissorOutlined />} className={styles.buttonmarginright}>粘贴参数</Button>
            <Button type="dashed" icon={<CheckCircleOutlined />} className={styles.buttonmarginright}>保存</Button>
            <Button type='dashed' icon={<DeleteOutlined />}>取消</Button>
          </Col>
        </Row>
        <Row gutter={24}>
          <Col lg={7} md={24}>
            <div>
              <GuidelineTree setDefault= {setSelectGuidelineId} />
            </div>
          </Col>
          <Col lg={17} md={24}>
            <Row gutter={24}>
              <Col span={24} >
                <GuidelineForm  guidelineData= {guidelineData} />
              </Col>
              <Col span={24}>
                <GuidelineTable  guidelineData= {guidelineData} />
              </Col>
            </Row>
          </Col>
        </Row>
      </GridContent>
    </PageContainer>
  );
}

export default GuidelineManage;
