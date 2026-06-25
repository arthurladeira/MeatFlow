import type { UpdateItemPedidoRequestDTO } from './UpdateItemPedidoRequestDTO';

export interface UpdatePedidoRequestDTO {
  idtComprador: string;
  dataPedido: string;
  itens: UpdateItemPedidoRequestDTO[];
}
