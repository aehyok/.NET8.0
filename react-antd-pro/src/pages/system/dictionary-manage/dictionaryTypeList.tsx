import React, { useRef } from 'react';
import { Button, message, Modal } from 'antd';
import type { ActionType, ProColumns} from '@ant-design/pro-table';
import { TableDropdown } from '@ant-design/pro-table';
import ProTable from '@ant-design/pro-table';
// @ts-ignore
import styles from './split.less';
import { request } from 'umi';
import TypeModal from './type-modal'
import { ExclamationCircleOutlined } from '@ant-design/icons';
import { deleteDictionaryType } from '@/services/ant-design-pro/dictionary'
type TypeProps = {
  typeCode: string;
  onChangeClick: (record: SYSTEM.DictionaryTypeItem) => void;
};

const DictionaryTypeList: React.FC<TypeProps> = (props) => {
  const { onChangeClick, typeCode } = props;
  const [isShowModal, setIsShowModal] = React.useState(false);
  const [editId, setEditId]= React.useState('')

  const actionRef = useRef<ActionType>();

  const addDictionaryType = () => {
    setIsShowModal(true)
    setEditId('')
  }

  const hiddenModal = () => {
    setIsShowModal(false)
  }

  // 删除当前字典类型
  const deleteClick = () => {
    console.log(editId, '-----删除---')
    Modal.confirm({
      title: '系统提示',
      icon: <ExclamationCircleOutlined />,
      content: '请确认是否删除该字典类型?',
      okText: '确认',
      onOk:() => {
        deleteDictionaryType(editId).then(result => {
          if(result?.data === -1) {
            message.warn('删除失败，请先删除子该字典类型下的字典')
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

  const editClick =() => {
    setIsShowModal(true)
  }

  const operationClick = (record: string) => {
    if(record === 'edit') {
      console.log('编辑')
      editClick()
      return
    }
    if(record=== 'delete') {
      console.log('删除')
      deleteClick()
      return
    }
  }

  const columns: ProColumns<SYSTEM.DictionaryTypeItem>[] = [
    {
      title: '字典类型',
      key: 'name',
      dataIndex: 'name',
    },
    {
      title: 'code',
      key: 'code',
      dataIndex: 'code',
      hideInTable: false,
    },
    {
      title: '操作',
      valueType: 'option',
      render: () =>[
        <TableDropdown
          key="actionGroup"
          onSelect={(record: any) => { operationClick(record)}}
          menus={[
            { key: 'edit', name: '编辑' },
            { key: 'delete', name: '删除' },
          ]}
        />,
      ],
    },
  ];
  return (
    <>
      <ProTable<SYSTEM.DictionaryTypeItem>
        columns={columns}
        // showHeader={false}
        request={(params, sorter, filter) => {
          // 表单搜索项会从 params 传入，传递给后端接口。
          console.log(params, sorter, filter);
          return request<any>('/so/api/Dictionary/getDictionaryTypeList');
        }}
        rowKey="id"
        rowClassName={(record) => {
          return record.code === typeCode ? styles['split-row-select-active'] : '';
        }}
        actionRef={actionRef}
        toolbar={{
          search: {
            onSearch: (value) => {
              alert(value);
            },
          },
          actions: [
            <Button key="list" type="primary" onClick={() => addDictionaryType()}>
              新建
            </Button>,
          ],
        }}
        options={false}
        pagination={false}
        search={false}
        onRow={(record) => {
          return {
            onClick: () => {
              console.log('record', record)
              setEditId(record.id)
              onChangeClick(record)
            },
          };
        }}
      />
      {
        isShowModal ? <TypeModal  modalVisible={isShowModal}  actionRef={actionRef} editId={editId} hiddenModal={hiddenModal}/> :
        ''
      }

    </>
  );
};

export default DictionaryTypeList
