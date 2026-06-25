import { http } from '../../../lib/http';
import type { UpdateCompradorRequestDTO } from '../dto/UpdateCompradorRequestDTO';
import type { CompradorResponseDTO } from '../dto/CompradorResponseDTO';

export async function updateComprador(
  id: string,
  dto: UpdateCompradorRequestDTO,
): Promise<CompradorResponseDTO> {
  const { data } = await http.put<CompradorResponseDTO>(`/Comprador/${id}`, dto);
  return data;
}
