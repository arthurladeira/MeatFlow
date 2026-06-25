import { http } from '../../../lib/http';
import type { CarneResponseDTO } from '../dto/CarneResponseDTO';

export async function getCarneById(id: string): Promise<CarneResponseDTO> {
  const { data } = await http.get<CarneResponseDTO>(`/Carne/${id}`);
  return data;
}
