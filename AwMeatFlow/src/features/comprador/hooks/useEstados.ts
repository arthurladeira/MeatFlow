import { useState, useEffect, useCallback } from 'react';
import type { EstadoResponseDTO } from '../dto/EstadoResponseDTO';
import { getEstados } from '../services/getEstados';

interface UseEstadosResult {
  estados: EstadoResponseDTO[];
  loading: boolean;
}

export function useEstados(): UseEstadosResult {
  const [estados, setEstados] = useState<EstadoResponseDTO[]>([]);
  const [loading, setLoading] = useState(true);

  const fetch = useCallback(async () => {
    try {
      const data = await getEstados();
      setEstados(data);
    } catch {
      setEstados([]);
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    fetch();
  }, [fetch]);

  return { estados, loading };
}
