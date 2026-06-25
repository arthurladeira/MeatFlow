import { http } from '../../../lib/http';
import type { CreateCompradorRequestDTO } from '../dto/CreateCompradorRequestDTO';
import type { CompradorResponseDTO } from '../dto/CompradorResponseDTO';

export async function createComprador(dto: CreateCompradorRequestDTO): Promise<CompradorResponseDTO> {
  const { data } = await http.post<CompradorResponseDTO>('/Comprador', dto);
  return data;
}
