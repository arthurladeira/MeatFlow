import { http } from '../../../lib/http';

export async function deleteComprador(id: string): Promise<void> {
  await http.delete(`/Comprador/${id}`);
}
