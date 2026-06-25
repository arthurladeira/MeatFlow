import { http } from '../../../lib/http';

export async function deletePedido(id: string): Promise<void> {
  await http.delete(`/Pedido/${id}`);
}
