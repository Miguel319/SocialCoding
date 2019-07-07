export interface Paginacion {
  paginaActual: number;
  elementosPorPagina: number;
  elementosTotales: number;
  paginasTotales: number;
}

export class ResultadoPaginado<T> {
  resultado: T;
  paginacion: Paginacion;
}
