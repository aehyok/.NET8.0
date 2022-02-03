import React, { useEffect, useRef, useState } from 'react';
import { Button, message, Modal } from 'antd';
import { ActionType, ProColumns } from '@ant-design/pro-table';
import ProTable from '@ant-design/pro-table';
import { request } from 'umi';
// @ts-ignore
import DictionaryModal from './modal'
import { ExclamationCircleOutlined } from '@ant-design/icons';
import { deleteDictionary } from '@/services/ant-design-pro/dictionary'
type ListProps = {
  typeCode: string;
  title: string | undefined;
};

const DictionaryList: React.FC<ListProps> = (props) => {
  const { typeCode, title } = props;
  console.log('--------DictionaryList--------',typeCode)

  const actionRef = useRef<ActionType>();
  const [isShowModal, setIsShowModal] = React.useState(false);
  const [editId, setEditId]= React.useState(undefined)
  const params = [typeCode]
  const addDictionaryClick = () => {
    setEditId(undefined)
    setIsShowModal(true)
  }

  const hiddenModal = () => {
    setIsShowModal(false)
  }

  // 删除当前字典
  const deleteClick = (id: string) => {
    console.log(editId, '-----删除---')
    Modal.confirm({
      title: '系统提示',
      icon: <ExclamationCircleOutlined />,
      content: '请确认是否删除该字典?',
      okText: '确认',
      onOk:() => {
        deleteDictionary(id).then(result => {
          if(result?.data === -1) {
            message.warn('删除失败，请先删除子该字典下的字典')
          } else {
            if(result?.code === 200) {
              message.success('删除成功')
              actionRef.current?.reload()
            }
          }
        })
      },
      onCancel : () => {console.log('取消')},
      cancelText: '取消',
    });
  }

    // 编辑当前菜单
    const editClick = (record: any) => {
      console.log(record, '-------编辑-----------')
      setEditId(record.id)
      setIsShowModal(true)
    }

  const columns: ProColumns<SYSTEM.DictionaryItem>[] = [
    {
      dataIndex: 'index',
      valueType: 'indexBorder',
      key:'index',
      width: 48,
    },
    {
      title: '名称',
      key: 'name',
      width: 80,
      dataIndex: 'name',
    },
    {
      title: '编码',
      key: 'code',
      width: 80,
      dataIndex: 'code',
    },
    {
      title: '排序',
      key: 'displayOrder',
      width: 80,
      dataIndex: 'displayOrder',
    },
    {
      title: '备注',
      key: 'remark',
      width: 80,
      dataIndex: 'remark',
    },
    {
      title: '操作',
      key: 'option',
      width: 80,
      valueType: 'option',
      render: (_,record) => [
      <a
          key="edit"
          onClick={() => {
            editClick(record)
          }}
        >
          编辑
        </a>,
        <a
        key="delete"
        onClick={() => {
          deleteClick(record.id)
        }}
      >
        删除
      </a>,
    ],
    },
  ];

  // useEffect(() => {
  //   const fetch = async() => {
  //     const source = await getDictionaryList(typeCode)
  //     console.log(source, 'source')
  //     setTableListDataSource(source.data);
  //   }

  //   fetch()
  //   console.log('fetch--request')

  // }, [typeCode]);

  return (
    <>
    <ProTable<SYSTEM.DictionaryItem>
      columns={columns}
      params={params}
      request={(params, sorter, filter) => {
        // 表单搜索项会从 params 传入，传递给后端接口。
        console.log('1-0-1',params, sorter, filter);
        return request<any>(`/so/api/Dictionary/GetDictionaryList?typeCode=${params[0]}`);
      }}
      rowKey="id"
      headerTitle={title}
      actionRef={actionRef}
      search={false}
      toolbar={{
        actions: [
          <Button key="lists" type="primary" onClick={() => { addDictionaryClick() }}>
            新建
          </Button>,
        ],
      }}
    />
    {
      isShowModal ? <DictionaryModal typeCode={typeCode} modalVisible={isShowModal}  actionRef={actionRef} editId={editId} hiddenModal={hiddenModal}/> :
      ''
    }
    </>
  );
};

export default DictionaryList
