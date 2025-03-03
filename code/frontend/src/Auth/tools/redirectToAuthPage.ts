export function redirectToAuthPage(authUrl: string, redirectUri: string, clientId: string) {
  const queryParameters = [
    `redirect_uri=${redirectUri}`,
    'response_type=code',
    'scope=openid',
    `client_id=${clientId}`
  ];
  const query = queryParameters.join('&');
  window.location.replace(`${authUrl}/login?${query}`);
}
