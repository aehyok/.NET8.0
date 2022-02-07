import React, { useState , useEffect, useImperativeHandle, forwardRef } from 'react';
import { Tree, Input } from 'antd';

import { GetChildGuideLines } from '@/services/guideline/api'
const { Search } = Input
interface DataNode {
  title: string;
  key: string;
  isLeaf?: boolean;
  children?: DataNode[];
}


const initTreeData: DataNode[] = [];

function updateTreeData(list: DataNode[], key: React.Key, children: DataNode[]): DataNode[] {
  return list.map(node => {
    if (node.key === key) {
      return {
        ...node,
        children,
      };
    }
    if (node.children) {
      return {
        ...node,
        children: updateTreeData(node.children, key, children),
      };
    }
    return node;
  });
}

const GuidelineTree= (props: any, ref: any) => {
  const {setDefault} = props
  const [treeData, setTreeData] = useState(initTreeData);

  const [selectNode, setSelectNode] = useState()
  const loadTreeList = async (id: any = "1", type: any ="one") => {
    console.log(id, 'id')
    const response = await GetChildGuideLines(id)

    const list: DataNode[] = response.data.map((item: { id: any; guideLineName: any; fatherID: any; }) => {
      return {
        key: item.id,
        title: item.guideLineName,
        fatherId: item.fatherID
      }
    })
    console.log(list, '--list--')
    if(type === 'one') {  // 初始化时加载
      setTreeData(list)
      // setDefault([list[0].key])
    } else {  // 点击节点虚拟加载
      setTreeData(origin => {
        console.log(origin, '原始的数据')
        return updateTreeData(origin, id, [
          ...list
        ]);
      });
    }
  }

  useEffect(() => {
    loadTreeList()
  },[])

  const refreshTree = (type: string) => {
    console.log(selectNode, type, 'type----')
    if(type == 'add') { // 添加后的刷新
      loadTreeList(selectNode?.key, "two")
    }
    if(type === 'delete') {
      loadTreeList(selectNode?.fatherId, "two")
    }
  }

  useImperativeHandle(ref, () => ({
    refreshTree: refreshTree
  }))

  const onLoadData = async({ key, children }: any) => {
    console.log(key, children, 'onloaddata')
    new Promise<void>(resolve => {
      // children字节点有值直接返回，代表已经加载过
      if (children) {
        resolve();
        return;
      }

      loadTreeList(key,'two')
      resolve();
    });
  }

  const onSelectClick = (selectedKeys: any, e: any) => {
    console.log(selectedKeys, e, 'selectedKeys-info--')
    setSelectNode(e.node)
    setDefault(selectedKeys)
  }

  return (
    <>
      <Search style={{ marginBottom: 8 }} placeholder="请输入指标id或名称" />
      <Tree loadData={onLoadData} treeData={treeData} onSelect = { (selectedKeys, e)=> { onSelectClick(selectedKeys, e)} } />
    </>
  )
};

export default forwardRef(GuidelineTree);


