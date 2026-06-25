import type { CreateItemPedidoRequestDTO } from './CreateItemPedidoRequestDTO';

export interface CreatePedidoRequestDTO {
  idtComprador: string;
  dataPedido: string;
  itens: CreateItemPedidoRequestDTO[];
}
