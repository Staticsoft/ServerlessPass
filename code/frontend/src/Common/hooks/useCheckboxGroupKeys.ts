import { useState } from 'react';

import { Option } from '../types';

export const useCheckboxGroupKeys = (options: Option[]) => {
  const [selectedKeys, setSelectedKeys] = useState<string[]>([]);

  const onChange = (key: string) => {
    setSelectedKeys(prevState => (prevState.includes(key) ? prevState.filter(k => k !== key) : [...prevState, key]));
  };
  const onToggleAll = () => {
    setSelectedKeys(prevState => {
      return prevState.length === 0 ? options.map(({ key }) => key) : [];
    });
  };

  return { selectedKeys, onChange, onToggleAll };
};
