import { PageContainer } from '@ant-design/pro-layout'
import LogicFlow from "@logicflow/core";
import "@logicflow/core/dist/style/index.css";
import { useEffect, useRef } from "react";
import userTask from './components/user-task';
import styles from './index.less'


const FlowManage = () => {

  const refContainer = useRef();
  useEffect(() => {
    const logicflow = new LogicFlow({
      container: refContainer.current,
      grid: true,
      width: 1000,
      height: 500
    });

    logicflow.register(userTask)
    logicflow.on('node:click', (data) => {
      console.log(data, 'node click')
    })
    const graphData = {
      nodes: [
        {
          id: "node_id_1",
          type: "rect",
          x: 100,
          y: 100,
          text: { x: 100, y: 100, value: '节点1' },
          properties: {}
        },
        {
          id: "node_id_2",
          type: "circle",
          x: 200,
          y: 300,
          text: { x: 200, y: 300, value: '节点2' },
          properties: {}
        },
        {
            type: 'UserTask',
            x: 500,
            y: 500
        }
      ],
      edges: [
        {
          id: "edge_id",
          type: "polyline",
          sourceNodeId: "node_id_1",
          targetNodeId: "node_id_2",
          text: { x: 139, y: 200, value: "连线" },
          startPoint: { x: 100, y: 140 },
          endPoint: { x: 200, y: 250 },
          pointsList: [ { x: 100, y: 140 }, { x: 100, y: 200 }, { x: 200, y: 200 }, { x: 200, y: 250 } ],
          properties: {}
        }
      ]
    }

    logicflow.render(
      graphData
    );
  }, []);

  return (
    <div>
      <PageContainer>
      <div className={styles.flowContainer} ref={refContainer} />;
      </PageContainer>
    </div>
  )
}

export default FlowManage
