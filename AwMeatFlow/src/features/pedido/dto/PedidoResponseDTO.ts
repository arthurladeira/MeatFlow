import type { ItemPedidoResponseDTO } from './ItemPedidoResponseDTO';

export interface PedidoResponseDTO {
  idtPedido: string;
  idtComprador: string;
  nomeComprador: string;
  dataPedido: string;
  datCriacao: string;
  datAtualizacao: string;
  itens: ItemPedidoResponseDTO[];
}
