import { useState, useCallback } from 'react';
import type { PedidoResponseDTO } from '../dto/PedidoResponseDTO';
import { getPedidoById } from '../services/getPedidoById';

interface UsePedidoByIdResult {
  fetch: (id: string) => Promise<PedidoResponseDTO | null>;
  loading: boolean;
  error: string | null;
}

export function usePedidoById(): UsePedidoByIdResult {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetch = useCallback(async (id: string) => {
    setLoading(true);
    setError(null);
    try {
      return await getPedidoById(id);
    } catch {
      setError('Erro ao carregar pedido.');
      return null;
    } finally {
      setLoading(false);
    }
  }, []);

  return { fetch, loading, error };
}
