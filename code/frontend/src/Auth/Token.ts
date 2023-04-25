export function getTokenFromUrl(): string | null {
  const urlParams = new URLSearchParams(window.location.search);

  return urlParams.get('code');
}

const tokenStorageKey = 'token';

export function getTokenFromStorage(): string | null {
  return sessionStorage.getItem(tokenStorageKey);
}

export function putTokenToStorage(token: string) {
  sessionStorage.setItem(tokenStorageKey, token);
}

export function removeTokenFromStorage() {
  sessionStorage.removeItem(tokenStorageKey);
}
