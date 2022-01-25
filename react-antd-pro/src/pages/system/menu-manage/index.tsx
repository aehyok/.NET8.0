import React, { useEffect, useRef, useState } from 'react';
import type { ActionType, ProColumns } from '@ant-design/pro-table';
import { EditableProTable } from '@ant-design/pro-table';
import { Button, message, Modal } from 'antd';
import MenuModal from './modal'
import { ExclamationCircleOutlined } from '@ant-design/icons';
import { PageContainer } from '@ant-design/pro-layout';
import { getMenuList } from '@/services/ant-design-pro/menu'

/**
 * @param {arr: array 原数组数组, id: number 父节点id}
 * @return {children: array 子数组}
 */
 function getChildren(arr, id) {
  const res = [];
  for (let item of arr) {
    if (item.fatherId === id) { // 找到当前id的子元素
      // 插入子元素，每个子元素的children通过回调生成
      res.push({...item, children: getChildren(arr, item.id)});
    }
  }
  return res;
}

export default () => {

  const actionRef = useRef<ActionType>();
  const [isModalVisible, setIsModalVisible] = React.useState(false);
  const [editId, setEditId] = React.useState<number>()

  const [dataSource, setDataSource] = useState<SYSTEM.MenuItem[]>(() =>[]);

  useEffect(()=> {
    console.log('getMenuList')
    getMenuList().then((result: any) => {
      setDataSource(result.data)
      console.log(result.data, 'result.data')
      const list = getChildren(result.data,'1')
      console.log(list, '------list-------')
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
                console.log('row', row)
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
