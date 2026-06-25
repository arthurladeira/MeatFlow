import { http } from '../../../lib/http';
import type { CarneResponseDTO } from '../dto/CarneResponseDTO';

export async function getCarnes(): Promise<CarneResponseDTO[]> {
  const { data } = await http.get<CarneResponseDTO[]>('/Carne');
  return data;
}
