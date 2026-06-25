import { http } from '../../../lib/http';
import type { PedidoResponseDTO } from '../dto/PedidoResponseDTO';

export async function getPedidoById(id: string): Promise<PedidoResponseDTO> {
  const { data } = await http.get<PedidoResponseDTO>(`/Pedido/${id}`);
  return data;
}
