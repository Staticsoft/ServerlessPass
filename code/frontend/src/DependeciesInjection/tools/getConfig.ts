import { Config } from '../types';

export const getConfig = async (): Promise<Config> => {
  return await (await fetch('/config.json')).json();
};
