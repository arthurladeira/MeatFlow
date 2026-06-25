import { http } from '../../../lib/http';
import type { CompradorResponseDTO } from '../dto/CompradorResponseDTO';

export async function getCompradores(): Promise<CompradorResponseDTO[]> {
  const { data } = await http.get<CompradorResponseDTO[]>('/Comprador');
  return data;
}
