import { Td } from '@chakra-ui/react';

interface TableCellProps {
  value: string;
}

export function TableCell({ value }: TableCellProps) {
  return <Td>{value}</Td>;
}
