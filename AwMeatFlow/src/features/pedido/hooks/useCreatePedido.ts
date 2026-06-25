import { useState, useCallback } from 'react';
import type { CreatePedidoRequestDTO } from '../dto/CreatePedidoRequestDTO';
import type { PedidoResponseDTO } from '../dto/PedidoResponseDTO';
import { createPedido } from '../services/createPedido';

interface UseCreatePedidoResult {
  mutate: (dto: CreatePedidoRequestDTO) => Promise<PedidoResponseDTO | null>;
  loading: boolean;
  error: string | null;
}

export function useCreatePedido(): UseCreatePedidoResult {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const mutate = useCallback(async (dto: CreatePedidoRequestDTO) => {
    setLoading(true);
    setError(null);
    try {
      return await createPedido(dto);
    } catch {
      setError('Erro ao criar pedido.');
      return null;
    } finally {
      setLoading(false);
    }
  }, []);

  return { mutate, loading, error };
}
