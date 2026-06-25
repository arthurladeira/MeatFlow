import { http } from '../../../lib/http';
import type { CreatePedidoRequestDTO } from '../dto/CreatePedidoRequestDTO';
import type { PedidoResponseDTO } from '../dto/PedidoResponseDTO';

export async function createPedido(dto: CreatePedidoRequestDTO): Promise<PedidoResponseDTO> {
  const { data } = await http.post<PedidoResponseDTO>('/Pedido', dto);
  return data;
}
