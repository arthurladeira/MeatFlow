import { useState, useEffect, useCallback } from 'react';
import type { CompradorResponseDTO } from '../dto/CompradorResponseDTO';
import { getCompradores } from '../services/getCompradores';

interface UseCompradoresResult {
  compradores: CompradorResponseDTO[];
  loading: boolean;
  error: string | null;
  refetch: () => Promise<void>;
}

export function useCompradores(): UseCompradoresResult {
  const [compradores, setCompradores] = useState<CompradorResponseDTO[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const fetch = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await getCompradores();
      setCompradores(data);
    } catch {
      setError('Erro ao carregar compradores.');
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    fetch();
  }, [fetch]);

  return { compradores, loading, error, refetch: fetch };
}
