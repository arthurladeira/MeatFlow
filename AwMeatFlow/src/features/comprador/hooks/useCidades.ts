import { useState, useEffect, useCallback } from 'react';
import type { CidadeResponseDTO } from '../dto/CidadeResponseDTO';
import { getCidades } from '../services/getCidades';

interface UseCidadesResult {
  cidades: CidadeResponseDTO[];
  loading: boolean;
}

export function useCidades(): UseCidadesResult {
  const [cidades, setCidades] = useState<CidadeResponseDTO[]>([]);
  const [loading, setLoading] = useState(true);

  const fetch = useCallback(async () => {
    try {
      const data = await getCidades();
      setCidades(data);
    } catch {
      setCidades([]);
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    fetch();
  }, [fetch]);

  return { cidades, loading };
}
