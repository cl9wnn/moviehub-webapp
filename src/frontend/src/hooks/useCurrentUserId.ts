import { parseJwt, type JwtPayload } from "../utils/parseJwt";

export function getCurrentUserId(): string | null {
  const token = localStorage.getItem("accessToken");
  if (!token) return null;

  const payload = parseJwt<JwtPayload>(token);
  if (
    payload &&
    typeof payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"] === "string"
  ) {
    return payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
  }

  return null;
}