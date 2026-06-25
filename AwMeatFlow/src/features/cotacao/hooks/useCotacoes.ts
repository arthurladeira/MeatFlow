import { useState, useEffect, useCallback } from 'react';
import type { TaxasCambio } from '../types';
import { getCotacoes } from '../services/getCotacoes';

interface UseCotacoesResult {
  cotacoes: TaxasCambio;
  loading: boolean;
  error: string | null;
}

export function useCotacoes(): UseCotacoesResult {
  const [cotacoes, setCotacoes] = useState<TaxasCambio>({});
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const fetch = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await getCotacoes();
      setCotacoes(data);
    } catch {
      setError('Erro ao carregar cotações de moedas.');
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    fetch();
  }, [fetch]);

  return { cotacoes, loading, error };
}
