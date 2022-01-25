import React from 'react';
import { useModel } from 'umi';

const Index = () => {
  const { add, minus, count } = useModel('demo', (ret) => ({
    add: ret.increment,
    minus: ret.decrement,
    count: ret.counter
  }));
  return <div>
    <div>
      <button onClick={add}>add by {count}</button>
      <button onClick={minus}>minus by {count}</button>
    </div>
  </div>;
};

export default Index;
