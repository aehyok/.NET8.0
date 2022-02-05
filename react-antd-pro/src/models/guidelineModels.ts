// counter.ts
import { useState } from 'react';

export default () => {
  const [model, setModel] = useState({});
  const [parameters, setParameters] = useState([]);
  const [columns, setColumns] = useState([]);
  const changeModel = (values: any) => {
    // console.log('changeMOdel', values);
    setModel(values)
  }

  const changeParameters = (values: any) => {
    setParameters(values)
    // console.log('changeParameters', values)
  }

  const changeColumns = (values: any) => {
    setColumns(values)
    // console.log('changeColumns', values)
  }

  return { model, changeModel, parameters, changeParameters, columns, changeColumns };
};
