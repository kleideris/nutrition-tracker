import Cookies from "js-cookie";

export function setCookie(
  name: string,
  value: string,
  options?: Cookies.CookieAttributes
) {
  Cookies.set(name, value, options);
}

export function getCookie(name: string) {
  return Cookies.get(name);
}

export function deleteCookie(name: string, options?: Cookies.CookieAttributes) {
  Cookies.remove(name, options);
}
