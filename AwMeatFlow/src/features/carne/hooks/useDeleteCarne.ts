import { useState, useCallback } from 'react';
import axios from 'axios';
import { deleteCarne } from '../services/deleteCarne';

interface DeleteResult {
  success: boolean;
  errorMessage: string | null;
}

interface UseDeleteCarneResult {
  mutate: (id: string) => Promise<DeleteResult>;
  loading: boolean;
  error: string | null;
}

export function useDeleteCarne(): UseDeleteCarneResult {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const mutate = useCallback(async (id: string): Promise<DeleteResult> => {
    setLoading(true);
    setError(null);
    try {
      await deleteCarne(id);
      return { success: true, errorMessage: null };
    } catch (err) {
      const axiosErr = axios.isAxiosError(err) ? err : null;
      const msg =
        axiosErr?.response?.status === 409
          ? (axiosErr.response.data?.message ?? 'Não é possível excluir esta carne.')
          : 'Erro ao excluir carne.';
      setError(msg);
      return { success: false, errorMessage: msg };
    } finally {
      setLoading(false);
    }
  }, []);

  return { mutate, loading, error };
}
