export default {
  namespace: 'count',
  state: 0,
  reducers: {
    add(count: number) {
      return count + 1;
    },
    minus(count: number) {
      return count - 1;
    },
  },
};
