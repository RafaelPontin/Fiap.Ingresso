import { User } from "./user";

export interface RetornoUser{
  "data": User,
  "title": string,
  "status": number,
  "erros": []
}
