import type { TaxasCambio } from '../types';

export function converterParaBRL(valor: number, moeda: string, cotacoes: TaxasCambio): number {
  if (moeda === 'BRL') return valor;
  const taxa = cotacoes[moeda];
  if (taxa == null) return valor;
  return valor * taxa;
}
