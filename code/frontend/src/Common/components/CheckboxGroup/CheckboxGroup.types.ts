import { Option } from '~/Common/types';

export interface CheckboxGroupProps {
  options: Option[];
  selectedKeys: string[];
  onChange: (key: string) => void;
  onToggleAll: () => void;
}
