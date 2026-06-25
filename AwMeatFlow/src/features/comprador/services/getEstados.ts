import { http } from '../../../lib/http';
import type { EstadoResponseDTO } from '../dto/EstadoResponseDTO';

export async function getEstados(): Promise<EstadoResponseDTO[]> {
  const { data } = await http.get<EstadoResponseDTO[]>('/Estado');
  return data;
}
