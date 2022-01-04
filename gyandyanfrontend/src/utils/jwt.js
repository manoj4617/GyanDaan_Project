export function decode(token) {
    if (typeof token !== 'string') {
      throw new Error('Token type is not valid');
    }
  
    const parts = token.split('.');
    return JSON.parse(atob(parts[1]));
  }
  
  export function isAlive(payload) {
    if (!payload.exp) {
      return true;
    }
  
    const expiry = new Date(payload.exp);
    const now = new Date();
  
    return now <= expiry;
  }
  
  export function isInRole(role, userInfo) {
    console.log(role, userInfo);
    if (userInfo && userInfo.Roles) {
      return Array.isArray(userInfo.Roles)
        ? userInfo.Roles.find((r) => r === role)  
        : userInfo.Roles === role;
    }
  
    return false;
  }