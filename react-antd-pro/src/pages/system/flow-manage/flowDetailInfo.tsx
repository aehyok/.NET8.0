import { PageContainer } from '@ant-design/pro-layout'
import LogicFlow from "@logicflow/core";
import { DndPanel, Menu, Control, Snapshot, NodeResize  } from '@logicflow/extension';
import "@logicflow/core/dist/style/index.css";
import '@logicflow/extension/lib/style/index.css'

import { useEffect, useRef } from "react";
import initStatus from './components/init-status';
import styles from './index.less'

const FlowDetail = () => {
  const refContainer = useRef();
  LogicFlow.use(DndPanel);
  LogicFlow.use(Menu);
  LogicFlow.use(Control);
  LogicFlow.use(Snapshot);
  LogicFlow.use(NodeResize);

  useEffect(() => {
    const lf = new LogicFlow({
      container: refContainer.current,
      grid: true,
      // width: 1000,
      // height: '100%',
      keyboard: {
        enabled: true
      }
    });

    lf.register(initStatus)
    lf.on('node:click', (data) => {
      console.log(data, 'node click', lf.getGraphData())
      // lf.getSnapshot() 下载图片

    })

    lf.on('edge:click', (data) => {
      console.log(data, 'edge click',lf)
    })
    lf.setPatternItems([
      {
        type: 'initStatus',
        text: '开始',
        label: '初始状态',
        icon: 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAYAAAH6ji2bAAAABGdBTUEAALGPC/xhBQAAAnBJREFUOBGdVL1rU1EcPfdGBddmaZLiEhdx1MHZQXApraCzQ7GKLgoRBxMfcRELuihWKcXFRcEWF8HBf0DdDCKYRZpnl7p0svLe9Zzbd29eQhTbC8nv+9zf130AT63jvooOGS8Vf9Nt5zxba7sXQwODfkWpkbjTQfCGUd9gIp3uuPP8bZ946g56dYQvnBg+b1HB8VIQmMFrazKcKSvFW2dQTxJnJdQ77urmXWOMBCmXM2Rke4S7UAW+/8ywwFoewmBps2tu7mbTdp8VMOkIRAkKfrVawalJTtIliclFbaOBqa0M2xImHeVIfd/nKAfVq/LGnPss5Kh00VEdSzfwnBXPUpmykNss4lUI9C1ga+8PNrBD5YeqRY2Zz8PhjooIbfJXjowvQJBqkmEkVnktWhwu2SM7SMx7Cj0N9IC0oQXRo8xwAGzQms+xrB/nNSUWVveI48ayrFGyC2+E2C+aWrZHXvOuz+CiV6iycWe1Rd1Q6+QUG07nb5SbPrL4426d+9E1axKjY3AoRrlEeSQo2Eu0T6BWAAr6COhTcWjRaYfKG5csnvytvUr/WY4rrPMB53Uo7jZRjXaG6/CFfNMaXEu75nG47X+oepU7PKJvvzGDY1YLSKHJrK7vFUwXKkaxwhCW3u+sDFMVrIju54RYYbFKpALZAo7sB6wcKyyrd+aBMryMT2gPyD6GsQoRFkGHr14TthZni9ck0z+Pnmee460mHXbRAypKNy3nuMdrWgVKj8YVV8E7PSzp1BZ9SJnJAsXdryw/h5ctboUVi4AFiCd+lQaYMw5z3LGTBKjLQOeUF35k89f58Vv/tGh+l+PE/wG0rgfIUbZK5AAAAABJRU5ErkJggg==',
      },
      {
        type: 'circle',
        text: '开始',
        label: '处理状态',
        icon: 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAYAAAH6ji2bAAAABGdBTUEAALGPC/xhBQAAAnBJREFUOBGdVL1rU1EcPfdGBddmaZLiEhdx1MHZQXApraCzQ7GKLgoRBxMfcRELuihWKcXFRcEWF8HBf0DdDCKYRZpnl7p0svLe9Zzbd29eQhTbC8nv+9zf130AT63jvooOGS8Vf9Nt5zxba7sXQwODfkWpkbjTQfCGUd9gIp3uuPP8bZ946g56dYQvnBg+b1HB8VIQmMFrazKcKSvFW2dQTxJnJdQ77urmXWOMBCmXM2Rke4S7UAW+/8ywwFoewmBps2tu7mbTdp8VMOkIRAkKfrVawalJTtIliclFbaOBqa0M2xImHeVIfd/nKAfVq/LGnPss5Kh00VEdSzfwnBXPUpmykNss4lUI9C1ga+8PNrBD5YeqRY2Zz8PhjooIbfJXjowvQJBqkmEkVnktWhwu2SM7SMx7Cj0N9IC0oQXRo8xwAGzQms+xrB/nNSUWVveI48ayrFGyC2+E2C+aWrZHXvOuz+CiV6iycWe1Rd1Q6+QUG07nb5SbPrL4426d+9E1axKjY3AoRrlEeSQo2Eu0T6BWAAr6COhTcWjRaYfKG5csnvytvUr/WY4rrPMB53Uo7jZRjXaG6/CFfNMaXEu75nG47X+oepU7PKJvvzGDY1YLSKHJrK7vFUwXKkaxwhCW3u+sDFMVrIju54RYYbFKpALZAo7sB6wcKyyrd+aBMryMT2gPyD6GsQoRFkGHr14TthZni9ck0z+Pnmee460mHXbRAypKNy3nuMdrWgVKj8YVV8E7PSzp1BZ9SJnJAsXdryw/h5ctboUVi4AFiCd+lQaYMw5z3LGTBKjLQOeUF35k89f58Vv/tGh+l+PE/wG0rgfIUbZK5AAAAABJRU5ErkJggg==',
      },
      {
        type: 'diamond',
        label: '条件判断',
        icon: 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABUAAAAVCAYAAAHeEJUAAAAABGdBTUEAALGPC/xhBQAAAvVJREFUOBGNVEFrE0EU/mY3bQoiFlOkaUJrQUQoWMGePLX24EH0IIoHKQiCV0G8iE1covgLiqA/QTzVm1JPogc9tIJYFaQtlhQxqYjSpunu+L7JvmUTU3AgmTfvffPNN++9WSA1DO182f6xwILzD5btfAoQmwL5KJEwiQyVbSVZ0IgRyV6PTpIJ81E5ZvqfHQR0HUOBHW4L5Et2kQ6Zf7iAOhTFAA8s0pEP7AXO1uAA52SbqGk6h/6J45LaLhO64ByfcUzM39V7ZiAdS2yCePPEIQYvTUHqM/n7dgQNfBKWPjpF4ISk8q3J4nB11qw6X8l+FsF3EhlkEMfrjIer3wJTLwS2aCNcj4DbGxXTw00JmAuO+Ni6bBxVUCvS5d9aa04+so4pHW5jLTywuXAL7jJ+D06sl82Sgl2JuVBQn498zkc2bGKxULHjCnSMadBKYDYYHAtsby1EQ5lNGrQd4Y3v4Zo0XdGEmDno46yCM9Tk+RiJmUYHS/aXHPNTcjxcbTFna000PFJHIVZ5lFRqRpJWk9/+QtlOUYJj9HG5pVFEU7zqIYDVsw2s+AJaD8wTd2umgSCCyUxgGsS1Y6TBwXQQTFuZaHcd8gAGioE90hlsY+wMcs30RduYtxanjMGal8H5dMW67dmT1JFtYUEe8LiQLRsPZ6IIc7A4J5tqco3T0pnv/4u0kyzrYUq7gASuEyI8VXKvB9Odytv6jS/PNaZBln0nioJG/AVQRZvApOdhjj3Jt8QC8Im09SafwdBdvIpztpxWxpeKCC+EsFdS8DCyuCn2munFpL7ctHKp+Xc5cMybeIyMAN33SPL3ZR9QV1XVwLyzHm6Iv0/yeUuUb7PPlZC4D4HZkeu6dpF4v9j9MreGtMbxMMRLIcjJic9yHi7WQ3yVKzZVWUr5UrViJvn1FfUlwe/KYVfYyWRLSGNu16hR01U9IacajXPei0wx/5BqgInvJN+MMNtNme7ReU9SBbgntovn0kKHpFg7UogZvaZiOue/q1SBo9ktHzQAAAAASUVORK5CYII=',
      },
      {
        type: 'initStatus',
        text: '结束',
        label: '结束结束',
        icon: 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAYAAAH6ji2bAAAABGdBTUEAALGPC/xhBQAAA1BJREFUOBFtVE1IVUEYPXOf+tq40Y3vPcmFIdSjIorWoRG0ERWUgnb5FwVhYQSl72oUoZAboxKNFtWiwKRN0M+jpfSzqJAQclHo001tKkjl3emc8V69igP3znzfnO/M9zcDcKT67azmjYWTwl9Vn7Vumeqzj1DVb6cleQY4oAVnIOPb+mKAGxQmKI5CWNJ2aLPatxWa3aB9K7/fB+/Z0jUF6TmMlFLQqrkECWQzOZxYGjTlOl8eeKaIY5yHnFn486xBustDjWT6dG7pmjHOJd+33t0iitTPkK6tEvjxq4h2MozQ6WFSX/LkDUGfFwfhEZj1Auz/U4pyAi5Sznd7uKzznXeVHlI/Aywmk6j7fsUsEuCGADrWARXXwjxWQsUbIupDHJI7kF5dRktg0eN81IbiZXiTESic50iwS+t1oJgL83jAiBupLDCQqwziaWSoAFSeIR3P5Xv5az00wyIn35QRYTwdSYbz8pH8fxUUAtxnFvYmEmgI0wYXUXcCCSpeEVpXlsRhBnCEATxWylL9+EKCAYhe1NGstUa6356kS9NVvt3DU2fd+Wtbm/+lSbylJqsqkSm9CRhvoJVlvKPvF1RKY/FcPn5j4UfIMLn8D4UYb54BNsilTDXKnF4CfTobA0FpoW/LSp306wkXM+XaOJhZaFkcNM82ASNAWMrhrUbRfmyeI1FvRBTpN06WKxa9BK0o2E4Pd3zfBBEwPsv9sQBnmLVbLEIZ/Xe9LYwJu/Er17W6HYVBc7vmuk0xUQ+pqxdom5Fnp55SiytXLPYoMXNM4u4SNSCFWnrVIzKG3EGyMXo6n/BQOe+bX3FClY4PwydVhthOZ9NnS+ntiLh0fxtlUJHAuGaFoVmttpVMeum0p3WEXbcll94l1wM/gZ0Ccczop77VvN2I7TlsZCsuXf1WHvWEhjO8DPtyOVg2/mvK9QqboEth+7pD6NUQC1HN/TwvydGBARi9MZSzLE4b8Ru3XhX2PBxf8E1er2A6516o0w4sIA+lwURhAON82Kwe2iDAC1Watq4XHaGQ7skLcFOtI5lDxuM2gZe6WFIotPAhbaeYlU4to5cuarF1QrcZ/lwrLaCJl66JBocYZnrNlvm2+MBCTmUymPrYZVbjdlr/BxlMjmNmNI3SAAAAAElFTkSuQmCC',
      }
    ]);

    const graphData = {
        "nodes": [
            {
                "id": "5565d72f-d1cd-470a-89bf-346bfa83396c",
                "type": "UserTask",
                "x": 1000,
                "y": 500,
                "properties": {}
            },
            {
                "id": "70258c1e-bb80-4067-98ee-ba7019f86090",
                "type": "UserTask",
                "x": 300,
                "y": 120,
                "properties": {},
                "text": {
                    "x": 300,
                    "y": 120,
                    "value": "初始状态"
                }
            },
            {
                "id": "c9566437-6e4f-47a2-a913-e26ad2623016",
                "type": "UserTask",
                "x": 300,
                "y": 380,
                "properties": {},
                "text": {
                    "x": 300,
                    "y": 380,
                    "value": "摸排登记状态"
                }
            },
            {
                "id": "cfa81721-53c3-4d4a-9087-d8d5c3d4a058",
                "type": "diamond",
                "x": 300,
                "y": 580,
                "properties": {},
                "text": {
                    "x": 300,
                    "y": 580,
                    "value": "是否通过"
                }
            },
            {
                "id": "1b640a1f-a09c-4d0d-8cc8-32e0028b766f",
                "type": "UserTask",
                "x": 550,
                "y": 570,
                "properties": {},
                "text": {
                    "x": 550,
                    "y": 570,
                    "value": "不通过"
                }
            },
            {
                "id": "6b7e581b-9cb2-48f7-8377-0a074e8a7767",
                "type": "UserTask",
                "x": 290,
                "y": 810,
                "properties": {},
                "text": {
                    "x": 290,
                    "y": 810,
                    "value": "摸排通过待评议"
                }
            }
        ],
        "edges": [
            {
                "id": "0e6b000f-ea06-457d-a66d-5141710af40b",
                "type": "polyline",
                "sourceNodeId": "70258c1e-bb80-4067-98ee-ba7019f86090",
                "targetNodeId": "c9566437-6e4f-47a2-a913-e26ad2623016",
                "startPoint": {
                    "x": 300,
                    "y": 170
                },
                "endPoint": {
                    "x": 300,
                    "y": 330
                },
                "properties": {},
                "text": {
                    "x": 300,
                    "y": 239,
                    "value": "登记"
                },
                "pointsList": [
                    {
                        "x": 300,
                        "y": 170
                    },
                    {
                        "x": 300,
                        "y": 330
                    }
                ]
            },
            {
                "id": "6eb2c0ee-3839-4f50-adba-30396e8abe23",
                "type": "polyline",
                "sourceNodeId": "c9566437-6e4f-47a2-a913-e26ad2623016",
                "targetNodeId": "cfa81721-53c3-4d4a-9087-d8d5c3d4a058",
                "startPoint": {
                    "x": 300,
                    "y": 430
                },
                "endPoint": {
                    "x": 300,
                    "y": 530
                },
                "properties": {},
                "pointsList": [
                    {
                        "x": 300,
                        "y": 430
                    },
                    {
                        "x": 300,
                        "y": 530
                    }
                ]
            },
            {
                "id": "ca6838c0-99e9-41b2-a4d4-8b71d5f2a23d",
                "type": "polyline",
                "sourceNodeId": "cfa81721-53c3-4d4a-9087-d8d5c3d4a058",
                "targetNodeId": "1b640a1f-a09c-4d0d-8cc8-32e0028b766f",
                "startPoint": {
                    "x": 330,
                    "y": 580
                },
                "endPoint": {
                    "x": 500,
                    "y": 570
                },
                "properties": {},
                "pointsList": [
                    {
                        "x": 330,
                        "y": 580
                    },
                    {
                        "x": 415,
                        "y": 580
                    },
                    {
                        "x": 415,
                        "y": 570
                    },
                    {
                        "x": 500,
                        "y": 570
                    }
                ]
            },
            {
                "id": "9f225a1a-76fe-4b57-a4aa-9fef28e52713",
                "type": "polyline",
                "sourceNodeId": "cfa81721-53c3-4d4a-9087-d8d5c3d4a058",
                "targetNodeId": "6b7e581b-9cb2-48f7-8377-0a074e8a7767",
                "startPoint": {
                    "x": 300,
                    "y": 630
                },
                "endPoint": {
                    "x": 290,
                    "y": 760
                },
                "properties": {},
                "text": {
                    "x": 300,
                    "y": 692,
                    "value": "通过"
                },
                "pointsList": [
                    {
                        "x": 300,
                        "y": 630
                    },
                    {
                        "x": 300,
                        "y": 695
                    },
                    {
                        "x": 290,
                        "y": 695
                    },
                    {
                        "x": 290,
                        "y": 760
                    }
                ]
            }
        ]
    }

    lf.render(
      graphData
    );
  }, []);

  return (
      <PageContainer>
      <div className={styles.flowContainer} ref={refContainer} />
      </PageContainer>
  )
}

export default FlowDetail
