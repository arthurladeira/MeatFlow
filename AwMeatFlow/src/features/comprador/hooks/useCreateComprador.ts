import { useState, useCallback } from 'react';
import type { CreateCompradorRequestDTO } from '../dto/CreateCompradorRequestDTO';
import type { CompradorResponseDTO } from '../dto/CompradorResponseDTO';
import { createComprador } from '../services/createComprador';

interface UseCreateCompradorResult {
  mutate: (dto: CreateCompradorRequestDTO) => Promise<CompradorResponseDTO | null>;
  loading: boolean;
  error: string | null;
}

export function useCreateComprador(): UseCreateCompradorResult {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const mutate = useCallback(async (dto: CreateCompradorRequestDTO) => {
    setLoading(true);
    setError(null);
    try {
      return await createComprador(dto);
    } catch {
      setError('Erro ao cadastrar comprador.');
      return null;
    } finally {
      setLoading(false);
    }
  }, []);

  return { mutate, loading, error };
}
