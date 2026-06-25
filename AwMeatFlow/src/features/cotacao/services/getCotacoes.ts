import axios from 'axios';
import type { AwesomeApiResponse, TaxasCambio } from '../types';

const AWESOME_API_URL =
  'https://economia.awesomeapi.com.br/json/last/USD-BRL,EUR-BRL';

export async function getCotacoes(): Promise<TaxasCambio> {
  const { data } = await axios.get<AwesomeApiResponse>(AWESOME_API_URL);
  return Object.values(data).reduce<TaxasCambio>((acc, item) => {
    acc[item.code] = parseFloat(item.bid);
    return acc;
  }, {});
}
