import { useState, useMemo } from 'react';
import { DataTable, type TableColumn } from '../../../components/ui/DataTable';
import { Button } from '../../../components/ui/Button';
import { calcularTotalPedido } from '../utils/calcularTotalPedido';
import { formatDate, formatCurrency, formatDateTime } from '../../../utils/formatters';
import type { PedidoResponseDTO } from '../dto/PedidoResponseDTO';
import type { TaxasCambio } from '../../cotacao/types';

function formatDataAtualizacao(value: string): string {
  if (!value || value.startsWith('0001')) return '—';
  return formatDateTime(value);
}

interface PedidoTableProps {
  pedidos: PedidoResponseDTO[];
  onEdit: (pedido: PedidoResponseDTO) => void;
  onDelete: (pedido: PedidoResponseDTO) => void;
  cotacoes: TaxasCambio;
  cotacoesLoading: boolean;
}

export function PedidoTable({ pedidos, onEdit, onDelete, cotacoes, cotacoesLoading }: PedidoTableProps) {
  const [filterComprador, setFilterComprador] = useState('');
  const [filterData, setFilterData] = useState('');

  const filtered = useMemo(() => {
    const comp = filterComprador.toLowerCase();
    return pedidos.filter((p) => {
      if (comp && !p.nomeComprador.toLowerCase().includes(comp)) return false;
      if (filterData && !p.dataPedido.startsWith(filterData)) return false;
      return true;
    });
  }, [pedidos, filterComprador, filterData]);

  const columns: TableColumn<PedidoResponseDTO>[] = [
    {
      key: 'id',
      header: 'ID',
      render: (row) => (
        <span style={{ fontFamily: 'monospace', fontSize: 11, color: 'var(--color-text-muted)' }}>
          {row.idtPedido.slice(0, 8)}…
        </span>
      ),
    },
    {
      key: 'nomeComprador',
      header: 'Comprador',
      render: (row) => row.nomeComprador,
    },
    {
      key: 'dataPedido',
      header: 'Data',
      render: (row) => formatDate(row.dataPedido),
    },
    {
      key: 'itens',
      header: 'Itens',
      render: (row) => (
        <span style={{ color: 'var(--color-text-muted)' }}>{row.itens.length}</span>
      ),
    },
    {
      key: 'valorTotal',
      header: 'Valor Total (BRL)',
      render: (row) => {
        if (cotacoesLoading) {
          return <span style={{ color: 'var(--color-text-muted)' }}>…</span>;
        }
        const total = calcularTotalPedido(row.itens, cotacoes);
        return (
          <span style={{ fontWeight: 600 }}>
            {total > 0 ? formatCurrency(total, 'BRL') : '—'}
          </span>
        );
      },
    },
    {
      key: 'datCriacao',
      header: 'Criado em',
      render: (row) => (
        <span style={{ color: 'var(--color-text-muted)', whiteSpace: 'nowrap' }}>
          {formatDateTime(row.datCriacao)}
        </span>
      ),
    },
    {
      key: 'datAtualizacao',
      header: 'Atualizado em',
      render: (row) => (
        <span style={{ color: 'var(--color-text-muted)', whiteSpace: 'nowrap' }}>
          {formatDataAtualizacao(row.datAtualizacao)}
        </span>
      ),
    },
    {
      key: 'actions',
      header: 'Ações',
      render: (row) => (
        <div className="table-actions">
          <Button size="sm" variant="secondary" onClick={() => onEdit(row)}>
            Editar
          </Button>
          <Button size="sm" variant="danger" onClick={() => onDelete(row)}>
            Excluir
          </Button>
        </div>
      ),
    },
  ];

  return (
    <>
      <div className="table-filter-bar">
        <input
          className="table-filter-input"
          placeholder="Filtrar por comprador…"
          value={filterComprador}
          onChange={(e) => setFilterComprador(e.target.value)}
        />
        <input
          type="date"
          className="table-filter-input"
          value={filterData}
          onChange={(e) => setFilterData(e.target.value)}
        />
      </div>
      <DataTable columns={columns} data={filtered} keyExtractor={(r) => r.idtPedido} />
    </>
  );
}
