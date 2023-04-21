import { Option } from '~/Common/types';

export type Column<DataType extends object> = {
  name: string;
  title: string;
  filterOptions?: Option[];
  getRowValue: (row: DataType) => string;
};

export type FilterSettings = {
  [name: string]: string[];
};

export type DataTableProps<DataType extends object> = {
  data: DataType[];
  columns: Column<DataType>[];
  getRowKey: (row: DataType) => string;
  onFilterChange?: (filterSettings: FilterSettings) => void;
};
