import { Meta, StoryFn } from '@storybook/react';

import { Column, DataTable } from '../components/DataTable';

import { patternFilterOptions } from '~/Passwords/data';

export default {
  title: 'Passwords/DataTable'
} as Meta;

type UnitConversion = {
  key: string;
  fromUnit: string;
  toUnit: string;
  factor: number;
};

const data: UnitConversion[] = [
  {
    key: 'inches',
    fromUnit: 'inches',
    toUnit: 'millimetres (mm)',
    factor: 25.4
  },
  {
    key: 'feet',
    fromUnit: 'feet',
    toUnit: 'centimetres (cm)',
    factor: 30.48
  },
  {
    key: 'yards',
    fromUnit: 'yards',
    toUnit: 'metres (m)',
    factor: 0.91444
  }
];

const columns: Column<UnitConversion>[] = [
  { name: 'fromUnit', title: 'To convert', getRowValue: row => row.fromUnit },
  {
    name: 'toUnit',
    title: 'Into',
    filterOptions: patternFilterOptions,
    getRowValue: row => row.toUnit
  },
  { name: 'factor', title: 'Multiply by', getRowValue: row => row.factor.toString() }
];

export const Default: StoryFn = () => {
  return <DataTable columns={columns} data={data} getRowKey={row => row.key} />;
};

Default.storyName = 'DataTable';
