import { http } from '../../../lib/http';
import type { UpdateCarneRequestDTO } from '../dto/UpdateCarneRequestDTO';
import type { CarneResponseDTO } from '../dto/CarneResponseDTO';

export async function updateCarne(id: string, dto: UpdateCarneRequestDTO): Promise<CarneResponseDTO> {
  const { data } = await http.put<CarneResponseDTO>(`/Carne/${id}`, dto);
  return data;
}
