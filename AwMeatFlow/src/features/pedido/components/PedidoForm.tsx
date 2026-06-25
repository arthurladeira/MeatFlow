import { useState, useMemo } from 'react';
import { Input } from '../../../components/ui/Input';
import { Select } from '../../../components/ui/Select';
import { Button } from '../../../components/ui/Button';
import { ItemPedidoRow } from './ItemPedidoRow';
import { formatCurrency } from '../../../utils/formatters';
import { converterParaBRL } from '../../cotacao/utils/converterParaBRL';
import type { PedidoResponseDTO } from '../dto/PedidoResponseDTO';
import type { CreatePedidoRequestDTO } from '../dto/CreatePedidoRequestDTO';
import type { UpdatePedidoRequestDTO } from '../dto/UpdatePedidoRequestDTO';
import type { UpdateItemPedidoRequestDTO } from '../dto/UpdateItemPedidoRequestDTO';
import type { CompradorResponseDTO } from '../../comprador/dto/CompradorResponseDTO';
import type { CarneResponseDTO } from '../../carne/dto/CarneResponseDTO';
import type { TaxasCambio } from '../../cotacao/types';

interface PedidoFormProps {
  initial?: PedidoResponseDTO;
  compradores: CompradorResponseDTO[];
  carnes: CarneResponseDTO[];
  onSubmit: (dto: CreatePedidoRequestDTO | UpdatePedidoRequestDTO) => Promise<void>;
  onCancel: () => void;
  loading: boolean;
  cotacoes?: TaxasCambio;
}

interface FormState {
  idtComprador: string;
  dataPedido: string;
}

interface FormErrors {
  idtComprador?: string;
  dataPedido?: string;
  itens?: string;
}

function emptyItem(): UpdateItemPedidoRequestDTO {
  return { idtCarne: '', quantidadeKg: 0, valorUnitario: 0, codigoMoeda: 'BRL' };
}

function toDateInput(iso: string): string {
  return iso ? iso.slice(0, 10) : '';
}

export function PedidoForm({
  initial,
  compradores,
  carnes,
  onSubmit,
  onCancel,
  loading,
  cotacoes,
}: PedidoFormProps) {
  const isEditing = Boolean(initial);

  const [form, setForm] = useState<FormState>({
    idtComprador: initial?.idtComprador ?? '',
    dataPedido: initial ? toDateInput(initial.dataPedido) : toDateInput(new Date().toISOString()),
  });

  const [itens, setItens] = useState<UpdateItemPedidoRequestDTO[]>(
    initial?.itens.map((i) => ({
      idtItemPedido: i.idtItemPedido,
      idtCarne: i.idtCarne,
      quantidadeKg: i.quantidadeKg,
      valorUnitario: i.valorUnitario,
      codigoMoeda: i.codigoMoeda,
    })) ?? [emptyItem()],
  );

  const [errors, setErrors] = useState<FormErrors>({});

  const compradorOptions = compradores.map((c) => ({
    value: c.idtComprador,
    label: c.nomeComprador,
  }));

  const totalBRL = useMemo(() => {
    if (!cotacoes) return null;
    return itens.reduce((acc, i) => {
      const subtotal = i.quantidadeKg * i.valorUnitario;
      return acc + converterParaBRL(subtotal, i.codigoMoeda, cotacoes);
    }, 0);
  }, [itens, cotacoes]);

  const totalOriginal = useMemo(
    () => itens.reduce((acc, i) => acc + i.quantidadeKg * i.valorUnitario, 0),
    [itens],
  );

  const moedaPrincipal = itens[0]?.codigoMoeda ?? 'BRL';

  function validate(): boolean {
    const next: FormErrors = {};
    if (!form.idtComprador) next.idtComprador = 'Comprador é obrigatório.';
    if (!form.dataPedido) next.dataPedido = 'Data é obrigatória.';
    const hasInvalid = itens.some(
      (i) => !i.idtCarne || i.quantidadeKg <= 0 || i.valorUnitario <= 0 || !i.codigoMoeda,
    );
    if (hasInvalid) next.itens = 'Preencha todos os campos de cada item corretamente.';
    setErrors(next);
    return Object.keys(next).length === 0;
  }

  function handleItemUpdate(index: number, updated: Partial<UpdateItemPedidoRequestDTO>) {
    setItens((prev) => prev.map((item, i) => (i === index ? { ...item, ...updated } : item)));
  }

  function handleItemRemove(index: number) {
    setItens((prev) => prev.filter((_, i) => i !== index));
  }

  function handleItemAdd() {
    setItens((prev) => [...prev, emptyItem()]);
  }

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    if (!validate()) return;

    if (isEditing) {
      await onSubmit({
        idtComprador: form.idtComprador,
        dataPedido: form.dataPedido,
        itens,
      } satisfies UpdatePedidoRequestDTO);
    } else {
      await onSubmit({
        idtComprador: form.idtComprador,
        dataPedido: form.dataPedido,
        itens: itens.map(({ idtCarne, quantidadeKg, valorUnitario, codigoMoeda }) => ({
          idtCarne,
          quantidadeKg,
          valorUnitario,
          codigoMoeda,
        })),
      } satisfies CreatePedidoRequestDTO);
    }
  }

  function renderTotal() {
    if (totalBRL !== null && totalBRL > 0) {
      return formatCurrency(totalBRL, 'BRL');
    }
    if (totalOriginal > 0) {
      return formatCurrency(totalOriginal, moedaPrincipal);
    }
    return null;
  }

  const totalFormatado = renderTotal();

  return (
    <form onSubmit={handleSubmit} noValidate>
      <div className="modal-body">
        <div className="form-grid">
          <Select
            label="Comprador"
            required
            options={compradorOptions}
            value={form.idtComprador}
            onChange={(v) => setForm((prev) => ({ ...prev, idtComprador: v }))}
            placeholder="Selecione o comprador"
            error={errors.idtComprador}
          />
          <Input
            label="Data do pedido"
            required
            type="date"
            value={form.dataPedido}
            onChange={(e) => setForm((prev) => ({ ...prev, dataPedido: e.target.value }))}
            error={errors.dataPedido}
          />
        </div>

        <div className="items-section">
          <div className="items-header">
            <span className="items-label">Itens do pedido</span>
            <Button type="button" variant="secondary" size="sm" onClick={handleItemAdd}>
              + Adicionar item
            </Button>
          </div>

          {itens.map((item, idx) => (
            <ItemPedidoRow
              key={idx}
              index={idx}
              item={item}
              carnes={carnes}
              onUpdate={handleItemUpdate}
              onRemove={handleItemRemove}
              canRemove={itens.length > 1}
              cotacoes={cotacoes}
            />
          ))}

          {errors.itens && <span className="form-error">{errors.itens}</span>}

          {totalFormatado && (
            <div className="order-total">
              Total estimado: {totalFormatado}
            </div>
          )}
        </div>
      </div>

      <div className="modal-footer">
        <Button type="button" variant="secondary" onClick={onCancel} disabled={loading}>
          Cancelar
        </Button>
        <Button type="submit" loading={loading}>
          {isEditing ? 'Salvar alterações' : 'Criar pedido'}
        </Button>
      </div>
    </form>
  );
}
