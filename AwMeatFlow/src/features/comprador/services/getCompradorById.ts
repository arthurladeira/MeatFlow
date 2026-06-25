import { http } from '../../../lib/http';
import type { CompradorResponseDTO } from '../dto/CompradorResponseDTO';

export async function getCompradorById(id: string): Promise<CompradorResponseDTO> {
  const { data } = await http.get<CompradorResponseDTO>(`/Comprador/${id}`);
  return data;
}
