const theme = {
  // 基础节点，统一配置，优先级低于下面单独配置
  baseNode: {
    fill: "rgb(255, 230, 204)",  // 背景填充颜色
    stroke: "rgb(24, 125, 255)",  //边框颜色
    // strokeDasharray: "3,3"
  },
  // 矩形
  rect: {
    fill: "#FFFFFF",
    // strokeDasharray: "1, 1", // 矩形虚线
    className: "custom-cls",
  },
  circle: {
    r: 10,
    fill: "#9a9b9c"
  },
  // 条件判断
  diamond: {
    fill: "#FFFFFF",
  },
  ellipse: {
    strokeWidth: 3
  },
  polygon: {
    strokeDasharray: "none"
  },
  anchor: {
    r: 3,
    fill: "#9a9312",
    hover: {
      fill: "red"
    }
  },
  // 节点文本
  nodeText: {
    fontSize: 20,
    // color: "red", // 字的颜色
    overflowMode: "autoWrap" // 自动换行
  },
  baseEdge: {
    strokeWidth: 100,
    // strokeDasharray: "3,3"
  },
  edgeText: {
    textWidth: 60,
    fontSize: 16,
    overflowMode: "autoWrap",
    background: {
      // fill: "#919810" 动作背景设置
    }
  },
  // edge连接线
  polyline: {
    // offset: 10,
    // strokeDasharray: "1,1",  虚线
    strokeWidth: 2
  },
  bezier: {
    stroke: "red",
    adjustLine: {
      strokeWidth: 2,
      stroke: "red"
    },
    adjustAnchor: {
      stroke: "blue",
      fill: "green"
    }
  },
  // 箭头设置
  arrow: {
    offset: 10, // 箭头长度（类似于div的高度）
    verticalLength: 5, // 箭头垂直于边的距离（类似于div的宽度）
    fill: "green",
    stroke: "green"  // 箭头颜色
  },
  anchorLine: {
    stroke: "red"
  },
  snapline: {
    stroke: "red"
  },
  edgeAdjust: {
    r: 10
  },
  outline: {
    stroke: "red",
    hover: {
      stroke: "green"
    }
  }
};

export default theme;
