import { useState, useEffect, useCallback } from 'react';
import type { CarneResponseDTO } from '../dto/CarneResponseDTO';
import { getCarnes } from '../services/getCarnes';

interface UseCarnesResult {
  carnes: CarneResponseDTO[];
  loading: boolean;
  error: string | null;
  refetch: () => Promise<void>;
}

export function useCarnes(): UseCarnesResult {
  const [carnes, setCarnes] = useState<CarneResponseDTO[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const fetch = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await getCarnes();
      setCarnes(data);
    } catch {
      setError('Erro ao carregar carnes.');
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    fetch();
  }, [fetch]);

  return { carnes, loading, error, refetch: fetch };
}
