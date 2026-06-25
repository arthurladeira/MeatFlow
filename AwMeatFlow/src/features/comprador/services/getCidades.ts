import { http } from '../../../lib/http';
import type { CidadeResponseDTO } from '../dto/CidadeResponseDTO';

export async function getCidades(): Promise<CidadeResponseDTO[]> {
  const { data } = await http.get<CidadeResponseDTO[]>('/Cidade');
  return data;
}
