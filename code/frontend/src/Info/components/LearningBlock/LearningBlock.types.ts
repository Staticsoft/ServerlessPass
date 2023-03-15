import { SvgIcon } from '@mui/material';

export type LearningBlockProps = LearningOption;

export interface LearningOption {
  icon: typeof SvgIcon;
  title: string;
  text: string;
}
