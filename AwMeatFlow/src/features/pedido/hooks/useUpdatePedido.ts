import { useState, useCallback } from 'react';
import type { UpdatePedidoRequestDTO } from '../dto/UpdatePedidoRequestDTO';
import type { PedidoResponseDTO } from '../dto/PedidoResponseDTO';
import { updatePedido } from '../services/updatePedido';

interface UseUpdatePedidoResult {
  mutate: (id: string, dto: UpdatePedidoRequestDTO) => Promise<PedidoResponseDTO | null>;
  loading: boolean;
  error: string | null;
}

export function useUpdatePedido(): UseUpdatePedidoResult {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const mutate = useCallback(async (id: string, dto: UpdatePedidoRequestDTO) => {
    setLoading(true);
    setError(null);
    try {
      return await updatePedido(id, dto);
    } catch {
      setError('Erro ao atualizar pedido.');
      return null;
    } finally {
      setLoading(false);
    }
  }, []);

  return { mutate, loading, error };
}
