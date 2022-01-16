import { PageContainer } from '@ant-design/pro-layout'
import LogicFlow from "@logicflow/core";
import "@logicflow/core/dist/style/index.css";
import { useEffect, useRef } from "react";

const FlowManage = () => {

  const refContainer = useRef();
  useEffect(() => {
    const logicflow = new LogicFlow({
      container: refContainer.current,
      grid: true,
      width: 1000,
      height: 500
    });
    logicflow.render();
  }, []);

  return (
    <div>
      <PageContainer>
      <div  ref={refContainer} />;
      </PageContainer>
    </div>
  )
}

export default FlowManage
