import { Row, Col, Button, message, Modal } from 'antd';
import {PageContainer ,GridContent } from '@ant-design/pro-layout';
import React, { useEffect } from 'react';
import GuidelineForm from './form'
import GuidelineTab from './tab'
import GuidelineModal from './modal'
import GuidelineTree from './tree'
import { GetGuidelineDefine , DelGuideLine, SaveGuideLine} from '@/services/guideline/api'
import styles from './index.less'
import { DeleteOutlined, ExclamationCircleOutlined, FileAddOutlined } from '@ant-design/icons';
import { useModel } from 'umi'
const GuidelineManage = () =>{

  const { models, parameters, columns, changeModel, changeParameters, changeColumns } = useModel('guidelineModels', (ret) => ({
    changeModel: ret.changeModel,
    changeParameters: ret.changeParameters,
    changeColumns: ret.changeColumns,
    models: ret.model,
    parameters: ret.parameters,
    columns: ret.columns
  }));

  const [isModalVisible, setIsModalVisible] = React.useState(false);

  const [selectGuidelineId, setSelectGuidelineId] = React.useState([])
  const [guidelineData, setGuidelineData] = React.useState({})

  const loadTreeList = async (id: any = "1") => {
    console.log(id, 'id')
    const response = await GetGuidelineDefine(id)
    console.log(response, '--detail--response--')
    if(response.data) {
      setGuidelineData(response.data)
      changeModel(response.data)
      changeParameters(response.data.parameters)
      changeColumns(response.data.resultGroups)
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
        message.success('删除指标成功')
        // TODO 待刷新左侧树
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

  const saveGuidelineClick = async() => {
    const result = await SaveGuideLine({
      ...models,
      parameters: parameters,
      ResultGroup: [
        {
          defaultStatus: "SHOW",
          displayOrder: 1,
          displayTitle: '默认组',
          groupName: 'DEFAULT',
          textAlign: 'CENTER',
          fields:[...columns]
        }
      ]
    })
    console.log(result.code,'result', result)
  }
  return (
    <PageContainer>
      <GuidelineModal modalVisible = {isModalVisible} hiddenModal = {setIsModalVisible} selectGuidelineId={selectGuidelineId} />
      <GridContent>
        <Row justify={'space-between'}>
          <Col style={{margin: '5px 0'}} >
            <Button type="primary" icon={<FileAddOutlined />} onClick={() => showModal()} className={styles.buttonmarginright}>添加指标</Button>
            <Button type="dashed" icon={<DeleteOutlined />} onClick={() => deleteGuidelineClick()}>删除指标</Button>
            {/* <Button type="dashed" icon={ <ExportOutlined />}>导入指标</Button>
            <Button type="dashed" icon={ <ImportOutlined />}>导出指标</Button> */}
           </Col>
           <Col style={{margin: '5px 0'}} >
            <Button icon={<FileAddOutlined />} type="primary" className={styles.buttonmarginright} onClick={() => saveGuidelineClick()}>保存</Button>

          </Col>
        </Row>
        <Row gutter={12}>
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
                <GuidelineTab  guidelineData= {guidelineData} />
              </Col>
            </Row>
          </Col>
        </Row>
      </GridContent>
    </PageContainer>
  );
}

export default GuidelineManage;
