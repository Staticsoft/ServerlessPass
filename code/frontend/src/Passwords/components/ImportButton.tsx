import { Button } from '@chakra-ui/react';
import { CloudUploadOutlined } from '@mui/icons-material';
import { ChangeEvent, FC, useRef } from 'react';

import { readFile } from '~/Common';
import { useLocale } from '~/locale';

interface Props {
  onImport: (passwordsJSON: string) => Promise<void>;
}

export const ImportButton: FC<Props> = ({ onImport }) => {
  const { buttons } = useLocale();

  const inputRef = useRef<HTMLInputElement>(null);

  const handlePasswordsImport = async (event: ChangeEvent<HTMLInputElement>) => {
    const file = event.target?.files?.[0];

    if (!file) return;

    const importJSON = await readFile(file);

    if (importJSON) await onImport(importJSON);
  };

  return (
    <>
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

      <input ref={inputRef} hidden type="file" accept=".json" onChange={handlePasswordsImport} />
    </>
  );
};
