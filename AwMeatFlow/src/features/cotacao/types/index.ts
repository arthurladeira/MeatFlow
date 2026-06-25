export interface CotacaoItem {
  code: string;
  codein: string;
  name: string;
  high: string;
  low: string;
  varBid: string;
  pctChange: string;
  bid: string;
  ask: string;
  timestamp: string;
  create_date: string;
}

export type AwesomeApiResponse = Record<string, CotacaoItem>;

/** Taxa de câmbio para BRL por código de moeda. Ex: { USD: 5.75, EUR: 6.20 } */
export type TaxasCambio = Record<string, number>;
