export interface Mensaje {
    id: number;
    remitenteId: number;
    remitenteAlias: string;
    remitenteImagenUrl: string;
    receptorId: number;
    receptorAlias: string;
    receptorImagenUrl: string;
    contenido: string;
    leido: boolean;
    leidoEn: Date;
    mensajeEnviado: Date;
}