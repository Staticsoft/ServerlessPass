import { Button, HStack, Stack } from '@chakra-ui/react';
import { CloudUploadOutlined } from '@mui/icons-material';
import { ChangeEvent, FC, useRef } from 'react';

import { readFile } from '~/Common';
import { ServerlessPassTitle } from '~/Info/components';
import { useLocale } from '~/locale';
import { PasswordsTable } from '~/Passwords/components';
import { UsePasswordsHook } from '~/Passwords/hooks';

import classes from './PasswordPage.styles.module.scss';

interface Props {
  usePasswords: UsePasswordsHook;
}

export const PasswordPage: FC<Props> = props => {
  const { usePasswords } = props;

  const { passwords, importPasswords } = usePasswords();

  const { buttons } = useLocale();

  const inputRef = useRef<HTMLInputElement>(null);

  const handlePasswordsImport = async (event: ChangeEvent<HTMLInputElement>) => {
    const file = event.target?.files?.[0];

    if (!file) return;

    const importJSON = await readFile(file);

    if (importJSON) await importPasswords(importJSON);
  };

  return (
    <div className={classes.page}>
      <Stack spacing={160}>
        <ServerlessPassTitle />

        <Stack spacing={10}>
          <HStack width={'100%'} justifyContent={'flex-end'}>
            <Button
              colorScheme={'messenger'}
              variant={'solid'}
              display={'flex'}
              gap={2}
              onClick={() => inputRef.current?.click()}
            >
              {buttons.import}

              <CloudUploadOutlined />
            </Button>

            <input ref={inputRef} hidden type="file" onChange={handlePasswordsImport} />
          </HStack>

          <PasswordsTable passwords={passwords} />
        </Stack>
      </Stack>
    </div>
  );
};
