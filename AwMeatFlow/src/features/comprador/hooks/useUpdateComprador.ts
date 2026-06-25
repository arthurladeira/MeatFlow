import { useState, useCallback } from 'react';
import type { UpdateCompradorRequestDTO } from '../dto/UpdateCompradorRequestDTO';
import type { CompradorResponseDTO } from '../dto/CompradorResponseDTO';
import { updateComprador } from '../services/updateComprador';

interface UseUpdateCompradorResult {
  mutate: (id: string, dto: UpdateCompradorRequestDTO) => Promise<CompradorResponseDTO | null>;
  loading: boolean;
  error: string | null;
}

export function useUpdateComprador(): UseUpdateCompradorResult {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const mutate = useCallback(async (id: string, dto: UpdateCompradorRequestDTO) => {
    setLoading(true);
    setError(null);
    try {
      return await updateComprador(id, dto);
    } catch {
      setError('Erro ao atualizar comprador.');
      return null;
    } finally {
      setLoading(false);
    }
  }, []);

  return { mutate, loading, error };
}
