import { useState, useCallback } from 'react';
import { deletePedido } from '../services/deletePedido';

interface UseDeletePedidoResult {
  mutate: (id: string) => Promise<boolean>;
  loading: boolean;
  error: string | null;
}

export function useDeletePedido(): UseDeletePedidoResult {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const mutate = useCallback(async (id: string) => {
    setLoading(true);
    setError(null);
    try {
      await deletePedido(id);
      return true;
    } catch {
      setError('Erro ao excluir pedido.');
      return false;
    } finally {
      setLoading(false);
    }
  }, []);

  return { mutate, loading, error };
}
