import { Imagen } from "./Imagen";

export interface Usuario {
  id: number;
  nombreUsuario: string;
  alias: string;
  edad: number;
  genero: string;
  creadoEn: string;
  fechaNacimiento: Date;
  ultimaSesion: Date;
  trabajaEn: string;
  imagenUrl: string;
  ciudad: string;
  pais: string;
  lenguajes: string;
  hobbies: string;
  imagenes?: Imagen[];
}
