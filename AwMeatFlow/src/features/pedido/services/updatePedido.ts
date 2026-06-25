import { http } from '../../../lib/http';
import type { UpdatePedidoRequestDTO } from '../dto/UpdatePedidoRequestDTO';
import type { PedidoResponseDTO } from '../dto/PedidoResponseDTO';

export async function updatePedido(id: string, dto: UpdatePedidoRequestDTO): Promise<PedidoResponseDTO> {
  const { data } = await http.put<PedidoResponseDTO>(`/Pedido/${id}`, dto);
  return data;
}
