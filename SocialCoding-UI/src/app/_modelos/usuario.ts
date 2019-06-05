import { Imagen } from './Imagen';

export interface Usuario {
    id: number;
    nombreUsuario: string;
    alias: string;
    edad: number;
    genero: string;
    creadoEn: string;
    ultimaSesion: Date;
    trabajaEn: string;
    imagenUrl: string;
    ciudad: string;
    pais: string;
    imagenes?: Imagen[];
}