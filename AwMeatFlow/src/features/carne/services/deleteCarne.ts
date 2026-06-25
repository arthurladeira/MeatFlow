import { http } from '../../../lib/http';

export async function deleteCarne(id: string): Promise<void> {
  await http.delete(`/Carne/${id}`);
}
