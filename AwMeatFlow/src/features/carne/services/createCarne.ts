import { http } from '../../../lib/http';
import type { CreateCarneRequestDTO } from '../dto/CreateCarneRequestDTO';
import type { CarneResponseDTO } from '../dto/CarneResponseDTO';

export async function createCarne(dto: CreateCarneRequestDTO): Promise<CarneResponseDTO> {
  const { data } = await http.post<CarneResponseDTO>('/Carne', dto);
  return data;
}
