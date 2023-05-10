export function redirectToAuthPage(authUrl: string) {
  window.location.replace(`${authUrl}/login?redirect_uri=${window.location.href}&response_type=code&scope=openid`);
}
