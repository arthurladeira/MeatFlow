import { useState, useCallback } from 'react';
import type { CarneResponseDTO } from '../dto/CarneResponseDTO';
import { getCarneById } from '../services/getCarneById';

interface UseCarneByIdResult {
  fetch: (id: string) => Promise<CarneResponseDTO | null>;
  loading: boolean;
  error: string | null;
}

export function useCarneById(): UseCarneByIdResult {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetch = useCallback(async (id: string) => {
    setLoading(true);
    setError(null);
    try {
      return await getCarneById(id);
    } catch {
      setError('Erro ao carregar carne.');
      return null;
    } finally {
      setLoading(false);
    }
  }, []);

  return { fetch, loading, error };
}
