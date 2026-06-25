import { http } from '../../../lib/http';
import type { PedidoResponseDTO } from '../dto/PedidoResponseDTO';

export async function getPedidos(): Promise<PedidoResponseDTO[]> {
  const { data } = await http.get<PedidoResponseDTO[]>('/Pedido');
  return data;
}
