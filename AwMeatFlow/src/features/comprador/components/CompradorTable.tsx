import { useState, useMemo } from 'react';
import { DataTable, type TableColumn } from '../../../components/ui/DataTable';
import { Button } from '../../../components/ui/Button';
import { formatDocument } from '../utils';
import { formatDateTime } from '../../../utils/formatters';
import type { CompradorResponseDTO } from '../dto/CompradorResponseDTO';

function formatDataAtualizacao(value: string): string {
  if (!value || value.startsWith('0001')) return '—';
  return formatDateTime(value);
}

interface CompradorTableProps {
  compradores: CompradorResponseDTO[];
  onEdit: (comprador: CompradorResponseDTO) => void;
  onDelete: (comprador: CompradorResponseDTO) => void;
}

export function CompradorTable({ compradores, onEdit, onDelete }: CompradorTableProps) {
  const [filterNome, setFilterNome] = useState('');
  const [filterDocumento, setFilterDocumento] = useState('');
  const [filterLocalidade, setFilterLocalidade] = useState('');

  const filtered = useMemo(() => {
    const nome = filterNome.toLowerCase();
    const doc = filterDocumento.replace(/\D/g, '');
    const loc = filterLocalidade.toLowerCase();
    return compradores.filter((c) => {
      if (nome && !c.nomeComprador.toLowerCase().includes(nome)) return false;
      if (doc && !c.documentoFiscal.includes(doc)) return false;
      if (loc && !`${c.nomeCidade} ${c.nomeEstado}`.toLowerCase().includes(loc)) return false;
      return true;
    });
  }, [compradores, filterNome, filterDocumento, filterLocalidade]);

  const columns: TableColumn<CompradorResponseDTO>[] = [
    {
      key: 'id',
      header: 'ID',
      render: (row) => (
        <span style={{ fontFamily: 'monospace', fontSize: 11, color: 'var(--color-text-muted)' }}>
          {row.idtComprador.slice(0, 8)}…
        </span>
      ),
    },
    {
      key: 'nomeComprador',
      header: 'Nome',
      render: (row) => row.nomeComprador,
    },
    {
      key: 'documentoFiscal',
      header: 'Documento',
      render: (row) => (
        <span style={{ fontFamily: 'monospace' }}>{formatDocument(row.documentoFiscal)}</span>
      ),
    },
    {
      key: 'localidade',
      header: 'Localidade',
      render: (row) => (
        <span style={{ color: 'var(--color-text-muted)' }}>
          {row.nomeCidade} / {row.nomeEstado}
        </span>
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
          placeholder="Filtrar por nome…"
          value={filterNome}
          onChange={(e) => setFilterNome(e.target.value)}
        />
        <input
          className="table-filter-input"
          placeholder="Filtrar por documento…"
          value={filterDocumento}
          onChange={(e) => setFilterDocumento(e.target.value)}
        />
        <input
          className="table-filter-input"
          placeholder="Filtrar por localidade…"
          value={filterLocalidade}
          onChange={(e) => setFilterLocalidade(e.target.value)}
        />
      </div>
      <DataTable columns={columns} data={filtered} keyExtractor={(r) => r.idtComprador} />
    </>
  );
}
