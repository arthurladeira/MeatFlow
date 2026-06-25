import { Select } from '../../../components/ui/Select';
import { Input } from '../../../components/ui/Input';
import { Button } from '../../../components/ui/Button';
import { MOEDAS, MOEDA_LABELS } from '../types';
import { formatCurrency } from '../../../utils/formatters';
import { converterParaBRL } from '../../cotacao/utils/converterParaBRL';
import type { UpdateItemPedidoRequestDTO } from '../dto/UpdateItemPedidoRequestDTO';
import type { CarneResponseDTO } from '../../carne/dto/CarneResponseDTO';
import type { TaxasCambio } from '../../cotacao/types';

interface ItemPedidoRowProps {
  item: UpdateItemPedidoRequestDTO;
  index: number;
  carnes: CarneResponseDTO[];
  onUpdate: (index: number, updated: Partial<UpdateItemPedidoRequestDTO>) => void;
  onRemove: (index: number) => void;
  canRemove: boolean;
  cotacoes?: TaxasCambio;
}

const moedaOptions = MOEDAS.map((m) => ({ value: m, label: MOEDA_LABELS[m] }));

export function ItemPedidoRow({ item, index, carnes, onUpdate, onRemove, canRemove, cotacoes }: ItemPedidoRowProps) {
  const carneOptions = carnes.map((c) => ({ value: c.idtCarne, label: c.descricaoCarne }));
  const subtotal = item.quantidadeKg * item.valorUnitario;
  const moeda = item.codigoMoeda || 'BRL';

  function renderSubtotal() {
    if (subtotal <= 0) return '—';
    const original = formatCurrency(subtotal, moeda);
    if (moeda === 'BRL' || !cotacoes) return original;
    const taxa = cotacoes[moeda];
    if (taxa == null) return original;
    const emBRL = converterParaBRL(subtotal, moeda, cotacoes);
    return `${original} → ${formatCurrency(emBRL, 'BRL')}`;
  }

  return (
    <div className="item-row">
      <Select
        label="Carne"
        placeholder="Selecione a carne"
        options={carneOptions}
        value={item.idtCarne}
        onChange={(v) => onUpdate(index, { idtCarne: v })}
      />
      <Input
        label="Qtd. (kg)"
        type="number"
        placeholder="0,00"
        min={0.01}
        step={0.01}
        value={item.quantidadeKg || ''}
        onChange={(e) => onUpdate(index, { quantidadeKg: parseFloat(e.target.value) || 0 })}
      />
      <Input
        label="Valor/kg"
        type="number"
        placeholder="0,00"
        min={0.01}
        step={0.01}
        value={item.valorUnitario || ''}
        onChange={(e) => onUpdate(index, { valorUnitario: parseFloat(e.target.value) || 0 })}
      />
      <Select
        label="Moeda"
        options={moedaOptions}
        value={item.codigoMoeda}
        onChange={(v) => onUpdate(index, { codigoMoeda: v })}
      />
      <span className="item-row-total">{renderSubtotal()}</span>
      <Button
        type="button"
        variant="ghost"
        size="sm"
        onClick={() => onRemove(index)}
        disabled={!canRemove}
        aria-label="Remover item"
        style={{ color: canRemove ? 'var(--color-danger)' : undefined }}
      >
        ✕
      </Button>
    </div>
  );
}
