import { Flex } from '@chakra-ui/react';
import { DoneAll, GetAppOutlined, LoginOutlined, SettingsOutlined } from '@mui/icons-material';

import { LearningBlock } from '../LearningBlock';

import { useLocale } from '~/locale';

import classes from './SetupGuide.styles.module.scss';

export const SetupGuide: React.FC = () => {
  const { root } = useLocale();

  return (
    <Flex
      className={classes.setupGuide}
      direction={{ sm: 'column', md: 'row' }}
      alignItems={'top'}
      justifyContent={'space-between'}
    >
      {root.setupGuide.steps.map(({ name, ...step }) => {
        return <LearningBlock key={name} icon={iconsMap[name as keyof typeof iconsMap]} {...step} />;
      })}
    </Flex>
  );
};

const iconsMap = {
  signUp: LoginOutlined,
  installLesspass: GetAppOutlined,
  configureLesspass: SettingsOutlined,
  youAreAllSet: DoneAll
};
