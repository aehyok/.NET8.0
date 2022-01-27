import React, { useEffect, useRef, useState } from 'react';
import type { ActionType, ProColumns } from '@ant-design/pro-table';
import { EditableProTable } from '@ant-design/pro-table';
import { Button, message, Modal } from 'antd';
import MenuModal from './modal'
import { ExclamationCircleOutlined } from '@ant-design/icons';
import { PageContainer } from '@ant-design/pro-layout';
import { getMenuList } from '@/services/ant-design-pro/menu'

export default () => {
  const actionRef = useRef<ActionType>();
  const rootId ='1'
  const [isModalVisible, setIsModalVisible] = React.useState(false);
  const [editId, setEditId] = React.useState<number>()

  const [dataSource, setDataSource] = useState<SYSTEM.MenuItem[]>(() =>[]);

  let treeList: any = null

  function listToTree<T extends COMMON.Recursion>(list: T[], fatherId: string|undefined, current: T | null){
    // 先根据fatherId找出父节点列表
    // 第一次 通过fatherid =1,查找出2，3，4节点列表
    // 第二次 通过fatherid =2 ,查找出5，6，7

    const parentList = list.filter((item: T) => item.fatherId === fatherId)
    console.log(parentList, 'parentList')
    // 第一次查找出来后将节点list放入到树中
    //
    if(current === null ||  treeList.length === 0) {
      treeList = parentList
    } else {
      current.children = parentList
    }

    // 第一次 开始循环 2，3，4
    parentList.forEach((item: T) => {
      listToTree(list, item.id, item)
    })
  }

  useEffect(()=> {
    console.log('getMenuList')
    getMenuList().then((result: any) => {
      const temp: SYSTEM.MenuItem | null = null
      console.log(dataSource, 'dataSource')
      listToTree<SYSTEM.MenuItem>(result.data, rootId, temp)
      console.log(treeList, 'treeList')
      setDataSource(treeList)
    })
  }, [])

  const editClick = (record: SYSTEM.MenuItem) => {
    console.log(record, '-------编辑-----------')
    setEditId(record.id)
    setIsModalVisible(true)
  }

  const addChildClick = (record: SYSTEM.MenuItem) => {
    console.log(record, '-----添加-----')
    setIsModalVisible(true)
  }

  const deleteClick = (record: SYSTEM.MenuItem) => {
    console.log(record, '-----删除---')
    Modal.confirm({
      title: '系统提示',
      icon: <ExclamationCircleOutlined />,
      content: '请确认是否删除该菜单（以及该指标节点下的指标）?',
      okText: '确认',
      onOk:() => {
        message.success('删除成功')
        actionRef.current?.reload()
      },
      onCancel : () => {console.log('取消')},
      cancelText: '取消',
    });

  }
  const columns: ProColumns<SYSTEM.MenuItem>[] = [
    {
      title: '菜单名称',
      dataIndex: 'menuName',
      width: '30%',
    },
    {
      title: '菜单路由',
      dataIndex: 'menuPath',
      width: '30%',
    },
    {
      title: '顺序',
      dataIndex: 'displayOrder',
      width: '30%',
    },
    {
      title: '状态',
      dataIndex: 'status',
      width: '30%',
    },
    {
      title: '操作',
      valueType: 'option',
      width: 200,
      render: (text, record) => [
        <a
          key="edit"
          onClick={ () => {editClick(record)}}
        >
          编辑
        </a>,
        <a
          key="删除"
          onClick={ () => {deleteClick(record)}}
        >
        删除
        </a>,
        <a
          key="addChild"
          onClick={ () => {addChildClick(record)}}
        >
          添加子菜单
        </a>,
      ],
    },
  ];

  const addRootMenuClick = () => {
    setIsModalVisible(true)
  }

  return (
    <>
      <PageContainer>
        <EditableProTable<SYSTEM.MenuItem>
          expandable={{
            // 使用 request 请求数据时无效
            defaultExpandAllRows: true,
          }}
          value={dataSource}
          actionRef={actionRef}
          rowKey="id"
          headerTitle="菜单树"
          recordCreatorProps={false}
          columns={columns}
          onChange={setDataSource}
          onRow={(row) => {
            return {
              onClick: () => {
                // console.log('row', row)
              },
            }
          }}
          toolbar={{
            actions: [
              <Button key="lists" type="primary" onClick={() => {addRootMenuClick()}}>
                添加根菜单
              </Button>,
            ],
          }}
        />
        {
          !isModalVisible ? '' :
          <MenuModal modalVisible = {isModalVisible} hiddenModal = {setIsModalVisible} editId ={editId} actionRef={actionRef}/>
        }
      </PageContainer>
    </>
  );
};
