import { useState, useCallback } from 'react';
import type { CreateCarneRequestDTO } from '../dto/CreateCarneRequestDTO';
import type { CarneResponseDTO } from '../dto/CarneResponseDTO';
import { createCarne } from '../services/createCarne';

interface UseCreateCarneResult {
  mutate: (dto: CreateCarneRequestDTO) => Promise<CarneResponseDTO | null>;
  loading: boolean;
  error: string | null;
}

export function useCreateCarne(): UseCreateCarneResult {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const mutate = useCallback(async (dto: CreateCarneRequestDTO) => {
    setLoading(true);
    setError(null);
    try {
      return await createCarne(dto);
    } catch {
      setError('Erro ao cadastrar carne.');
      return null;
    } finally {
      setLoading(false);
    }
  }, []);

  return { mutate, loading, error };
}
