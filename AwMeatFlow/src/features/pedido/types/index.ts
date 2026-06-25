export type CodigoMoeda = 'BRL' | 'USD' | 'EUR';

export const MOEDAS: CodigoMoeda[] = ['BRL', 'USD', 'EUR'];

export const MOEDA_LABELS: Record<CodigoMoeda, string> = {
  BRL: 'BRL — Real',
  USD: 'USD — Dólar',
  EUR: 'EUR — Euro',
};
