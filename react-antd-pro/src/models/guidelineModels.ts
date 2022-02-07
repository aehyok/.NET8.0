import { useState } from 'react';

export default () => {
  const [model, setModel] = useState({});
  const [parameters, setParameters] = useState([]);
  const [columns, setColumns] = useState([]);
  const changeModel = (values: any) => {
    setModel(values);
  };

  const changeParameters = (values: any) => {
    setParameters(values);
  };

  const changeColumns = (values: any) => {
    setColumns(values);
  };

  return { model, changeModel, parameters, changeParameters, columns, changeColumns };
};
