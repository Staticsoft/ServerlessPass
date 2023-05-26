export function redirectToAuthPage(authUrl: string, redirectUri: string) {
  window.location.replace(`${authUrl}/login?redirect_uri=${redirectUri}&response_type=code&scope=openid`);
}
