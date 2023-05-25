import { useState, useEffect } from 'react';

import { getConfig } from '../tools';
import { Config } from '../types';

export const useConfig = () => {
  const [config, setConfig] = useState<Config>({
    redirectUri: '',
    backend: '',
    auth: '',
    clientId: ''
  });
  const [loadingConfig, setLoading] = useState(true);

  useEffect(() => {
    (async () => {
      setLoading(true);

      const newConfig = await getConfig();
      setConfig(newConfig);

      setLoading(false);
    })();
  }, []);

  return { config, loadingConfig };
};
