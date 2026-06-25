import { useState, useCallback } from 'react';
import type { CompradorResponseDTO } from '../dto/CompradorResponseDTO';
import { getCompradorById } from '../services/getCompradorById';

interface UseCompradorByIdResult {
  fetch: (id: string) => Promise<CompradorResponseDTO | null>;
  loading: boolean;
  error: string | null;
}

export function useCompradorById(): UseCompradorByIdResult {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetch = useCallback(async (id: string) => {
    setLoading(true);
    setError(null);
    try {
      return await getCompradorById(id);
    } catch {
      setError('Erro ao carregar comprador.');
      return null;
    } finally {
      setLoading(false);
    }
  }, []);

  return { fetch, loading, error };
}
