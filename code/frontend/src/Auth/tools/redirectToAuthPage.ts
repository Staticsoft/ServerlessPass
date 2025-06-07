export function redirectToAuthPage(authUrl: string, redirectUri: string, clientId: string) {
  window.location.replace(
    `${authUrl}/login?redirect_uri=${redirectUri}&response_type=code&scope=openid&client_id=${clientId}&state=/`
  );
}
