import { useState, useMemo } from 'react';
import { DataTable, type TableColumn } from '../../../components/ui/DataTable';
import { Button } from '../../../components/ui/Button';
import { getOrigemBadgeClass } from '../utils';
import { formatDateTime } from '../../../utils/formatters';
import type { CarneResponseDTO } from '../dto/CarneResponseDTO';

function formatDataAtualizacao(value: string): string {
  if (!value || value.startsWith('0001')) return '—';
  return formatDateTime(value);
}

const ORIGENS = ['Bovina', 'Suína', 'Aves', 'Peixes'];

interface CarneTableProps {
  carnes: CarneResponseDTO[];
  onEdit: (carne: CarneResponseDTO) => void;
  onDelete: (carne: CarneResponseDTO) => void;
}

export function CarneTable({ carnes, onEdit, onDelete }: CarneTableProps) {
  const [filterDescricao, setFilterDescricao] = useState('');
  const [filterOrigem, setFilterOrigem] = useState('');

  const filtered = useMemo(() => {
    const desc = filterDescricao.toLowerCase();
    return carnes.filter((c) => {
      if (desc && !c.descricaoCarne.toLowerCase().includes(desc)) return false;
      if (filterOrigem && c.origemCarne !== filterOrigem) return false;
      return true;
    });
  }, [carnes, filterDescricao, filterOrigem]);

  const columns: TableColumn<CarneResponseDTO>[] = [
    {
      key: 'id',
      header: 'ID',
      render: (row) => (
        <span style={{ fontFamily: 'monospace', fontSize: 11, color: 'var(--color-text-muted)' }}>
          {row.idtCarne.slice(0, 8)}…
        </span>
      ),
    },
    {
      key: 'descricaoCarne',
      header: 'Descrição',
      render: (row) => row.descricaoCarne,
    },
    {
      key: 'origemCarne',
      header: 'Origem',
      render: (row) => (
        <span className={getOrigemBadgeClass(row.origemCarne)}>{row.origemCarne}</span>
      ),
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
          placeholder="Filtrar por descrição…"
          value={filterDescricao}
          onChange={(e) => setFilterDescricao(e.target.value)}
        />
        <select
          className="table-filter-input"
          value={filterOrigem}
          onChange={(e) => setFilterOrigem(e.target.value)}
        >
          <option value="">Todas as origens</option>
          {ORIGENS.map((o) => (
            <option key={o} value={o}>
              {o}
            </option>
          ))}
        </select>
      </div>
      <DataTable columns={columns} data={filtered} keyExtractor={(r) => r.idtCarne} />
    </>
  );
}
