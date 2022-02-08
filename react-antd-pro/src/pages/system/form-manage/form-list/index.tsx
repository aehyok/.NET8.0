import { ExclamationCircleOutlined } from '@ant-design/icons';
import ProTable, { ActionType, ProColumns, TableDropdown } from '@ant-design/pro-table';
import { Button, message, Modal } from 'antd';
import React, { useRef } from 'react';
import { request, useModel } from 'umi';
import { deleteSystemForm } from '@/services/ant-design-pro/form'
import FormModel from '../modal'

const FormList = (props: any) => {
  console.log(props)

  const {  editId, setEditId } = useModel("formModels", (ret: { editId: any; setEditId: any; }) => ({
    setEditId: ret.setEditId,
    editId: ret.editId
  }))
  const [isModalVisible, setIsModalVisible] = React.useState(false);
  const actionRef = useRef<ActionType>();
  const addFormClick = () => {
    console.log('添加表单')
    setIsModalVisible(true)
  }

  const deleteFormClick = (record: any) => {
    console.log('删除表单', record)
    Modal.confirm({
      title: '系统提示',
      icon: <ExclamationCircleOutlined />,
      content: '请确认是否删除该菜单?',
      okText: '确认',
      onOk:() => {
        deleteSystemForm(record.id).then(result => {
          if(result?.data === -1) {
            message.warn('删除失败，请先删除子菜单')
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

  const refresh = () => {
    actionRef.current?.reload()
  }

  const columns: ProColumns<SYSTEM.FormItem>[] = [
    {
      title: '表单名称',
      key: 'formName',
      dataIndex: 'formName',
    },
    {
      title: '操作',
      valueType: 'option',
      render: (_: any,record: any) =>[
        <a key="delete" onClick={()=> {deleteFormClick(record)}}> 删除</a>
      ],
    },
  ];

  const setRowClassName = (record: any) => {
    return record.id === editId ? 'clickRowStyle' : '';
  }
  return <>
    <FormModel modalVisible = {isModalVisible} hiddenModal = {setIsModalVisible} refresh={refresh}/>
    <ProTable<SYSTEM.FormItem>
      rowKey="id"
      columns={columns}
      actionRef={actionRef}
      options={false}
      pagination={false}
      search={false}
      rowClassName={(record: any)=> {return setRowClassName(record)}}
      toolbar={{
        // search: {
        //   onSearch: (value) => {
        //     alert(value);
        //   },
        // },
        actions: [
          <Button key="list" type="primary" onClick={ () => { addFormClick()} }>
            新建
          </Button>,
        ],
      }}
      request={(params, sorter, filter) => {
        // 表单搜索项会从 params 传入，传递给后端接口。
        console.log(params, sorter, filter);
        return request<any>('/so/api/Form/GetSystemFormList');
      }}
      onRow={(record: any) => {
        return {
          onClick: () => {
            console.log('record', record)
            setEditId(record.id)
            // onChangeClick(record)
          },
        };
      }}
    />
  </>;
};

export default FormList;
