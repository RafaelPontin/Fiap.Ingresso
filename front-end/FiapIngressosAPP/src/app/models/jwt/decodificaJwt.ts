export interface DecodificaJwt{
  "unique_name": string,
  "sub": string,
  "email": string,
  "role": string,
  "nbf": number,
  "exp": number,
  "iat": number,
  "iss": string,
  "aud": string
}
