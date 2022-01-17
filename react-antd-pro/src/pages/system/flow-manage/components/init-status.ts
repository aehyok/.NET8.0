import { h, RectNode, RectNodeModel } from "@logicflow/core";

class UserTaskModel extends RectNodeModel {
  setAttributes() {
    this.width = 100;
    this.height = 100;
    this.radius =20; // 矩形的圆角  r为圆形的半径
  }
  getNodeStyle() {
    const style = super.getNodeStyle();
    const properties = this.properties;
    if (properties.statu === 'pass') {
      style.stroke = "green";
    } else if (properties.statu === 'reject') {
      style.stroke = "red";
    } else {
      style.stroke = "rgb(24, 125, 255)";
      // style.stroke ='red';
    }
    return style;
  }
}

class UserTaskView extends RectNode {
  private getLabelShape() {
    const { model } = this.props;
    const { x, y, width, height } = model;
    const style = model.getNodeStyle();
    return h(
      "svg",
      {
        x: x - width / 2 + 5,
        y: y - height / 2 + 5,
        width: 25,
        height: 25,
        viewBox: "0 0 1274 1024"
      },
      h("path", {
        fill: style.stroke,
        d:
          "M655.807326 287.35973m-223.989415 0a218.879 218.879 0 1 0 447.978829 0 218.879 218.879 0 1 0-447.978829 0ZM1039.955839 895.482975c-0.490184-212.177424-172.287821-384.030443-384.148513-384.030443-211.862739 0-383.660376 171.85302-384.15056 384.030443L1039.955839 895.482975z"
      })
    );
  }
  /**
   * 完全自定义节点外观方法
   */
  getShape() {
    const { model } = this.props;
    const { x, y, width, height, radius } = model;
    const style = model.getNodeStyle();
    return h("g", {}, [
      h("rect", {
        ...style,
        x: x - width / 2,
        y: y - height / 2,
        rx: radius,
        ry: radius,
        width,
        height
      }),
      this.getLabelShape()
    ]);
  }
}

export default {
  type: "UserTask",
  view: UserTaskView,
  model: UserTaskModel
};
