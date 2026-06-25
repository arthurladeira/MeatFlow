import type { ItemPedidoResponseDTO } from '../dto/ItemPedidoResponseDTO';
import type { TaxasCambio } from '../../cotacao/types';
import { converterParaBRL } from '../../cotacao/utils/converterParaBRL';

export function calcularTotalPedido(
  itens: ItemPedidoResponseDTO[],
  cotacoes?: TaxasCambio,
): number {
  return itens.reduce((acc, item) => {
    const subtotal = item.quantidadeKg * item.valorUnitario;
    const subtotalBRL = cotacoes
      ? converterParaBRL(subtotal, item.codigoMoeda, cotacoes)
      : subtotal;
    return acc + subtotalBRL;
  }, 0);
}
