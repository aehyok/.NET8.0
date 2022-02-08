import { useState } from 'react';

export default () => {
  const [model, setModel] = useState({});
  const [editId, setEditId] = useState('');
  const [columns, setColumns] = useState<any>([]);

  const changeModel = (values: any) => {
    setModel(values);
  };

  const changeColumns = (values: any) => {
    setColumns(values);
  };
  return { model, changeModel, editId, setEditId, columns, changeColumns };
};
