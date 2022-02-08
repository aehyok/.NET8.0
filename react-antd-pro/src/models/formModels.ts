import { useState } from 'react';

export default () => {
  const [model, setModel] = useState({});
  const [columns, setColumns] = useState([]);
  const changeModel = (values: any) => {
    setModel(values);
  };

  const changeColumns = (values: any) => {
    setColumns(values);
  };

  return { model, changeModel, columns, changeColumns };
};
