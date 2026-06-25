import { useState, useEffect, useCallback } from 'react';
import type { PedidoResponseDTO } from '../dto/PedidoResponseDTO';
import { getPedidos } from '../services/getPedidos';

interface UsePedidosResult {
  pedidos: PedidoResponseDTO[];
  loading: boolean;
  error: string | null;
  refetch: () => Promise<void>;
}

export function usePedidos(): UsePedidosResult {
  const [pedidos, setPedidos] = useState<PedidoResponseDTO[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const fetch = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await getPedidos();
      setPedidos(data);
    } catch {
      setError('Erro ao carregar pedidos.');
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    fetch();
  }, [fetch]);

  return { pedidos, loading, error, refetch: fetch };
}
