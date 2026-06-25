import { useState, useCallback } from 'react';
import type { UpdateCarneRequestDTO } from '../dto/UpdateCarneRequestDTO';
import type { CarneResponseDTO } from '../dto/CarneResponseDTO';
import { updateCarne } from '../services/updateCarne';

interface UseUpdateCarneResult {
  mutate: (id: string, dto: UpdateCarneRequestDTO) => Promise<CarneResponseDTO | null>;
  loading: boolean;
  error: string | null;
}

export function useUpdateCarne(): UseUpdateCarneResult {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const mutate = useCallback(async (id: string, dto: UpdateCarneRequestDTO) => {
    setLoading(true);
    setError(null);
    try {
      return await updateCarne(id, dto);
    } catch {
      setError('Erro ao atualizar carne.');
      return null;
    } finally {
      setLoading(false);
    }
  }, []);

  return { mutate, loading, error };
}
