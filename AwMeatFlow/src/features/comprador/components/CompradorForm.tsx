import { useState, useEffect, useMemo } from 'react';
import { Input } from '../../../components/ui/Input';
import { Select } from '../../../components/ui/Select';
import { Button } from '../../../components/ui/Button';
import { useEstados } from '../hooks/useEstados';
import { useCidades } from '../hooks/useCidades';
import { formatDocument } from '../utils';
import type { CompradorResponseDTO } from '../dto/CompradorResponseDTO';
import type { CreateCompradorRequestDTO } from '../dto/CreateCompradorRequestDTO';
import type { UpdateCompradorRequestDTO } from '../dto/UpdateCompradorRequestDTO';

interface CompradorFormProps {
  initial?: CompradorResponseDTO;
  onSubmit: (dto: CreateCompradorRequestDTO | UpdateCompradorRequestDTO) => Promise<void>;
  onCancel: () => void;
  loading: boolean;
}

interface FormState {
  nomeComprador: string;
  documentoFiscal: string;
  idtEstado: string;
  idtCidade: string;
}

interface FormErrors {
  nomeComprador?: string;
  documentoFiscal?: string;
  idtEstado?: string;
  idtCidade?: string;
}

export function CompradorForm({ initial, onSubmit, onCancel, loading }: CompradorFormProps) {
  const { estados, loading: loadingEstados } = useEstados();
  const { cidades, loading: loadingCidades } = useCidades();

  const [form, setForm] = useState<FormState>({
    nomeComprador: initial?.nomeComprador ?? '',
    documentoFiscal: initial?.documentoFiscal ?? '',
    idtEstado: '',
    idtCidade: initial?.idtCidade ?? '',
  });

  const [errors, setErrors] = useState<FormErrors>({});

  // Ao editar, assim que as cidades carregarem, deriva o estado a partir da cidade selecionada
  useEffect(() => {
    if (!initial?.idtCidade || cidades.length === 0) return;
    const cidade = cidades.find((c) => c.idtCidade === initial.idtCidade);
    if (cidade) {
      setForm((prev) => ({ ...prev, idtEstado: cidade.idtEstado }));
    }
  }, [initial?.idtCidade, cidades]);

  const estadoOptions = useMemo(
    () =>
      [...estados]
        .sort((a, b) => a.nomeEstado.localeCompare(b.nomeEstado))
        .map((e) => ({ value: e.idtEstado, label: `${e.siglaEstado} — ${e.nomeEstado}` })),
    [estados],
  );

  const cidadeOptions = useMemo(
    () =>
      cidades
        .filter((c) => c.idtEstado === form.idtEstado)
        .sort((a, b) => a.nomeCidade.localeCompare(b.nomeCidade))
        .map((c) => ({ value: c.idtCidade, label: c.nomeCidade })),
    [cidades, form.idtEstado],
  );

  function handleEstado(idtEstado: string) {
    setForm((prev) => ({ ...prev, idtEstado, idtCidade: '' }));
    setErrors((prev) => ({ ...prev, idtEstado: undefined, idtCidade: undefined }));
  }

  function validate(): boolean {
    const next: FormErrors = {};
    if (!form.nomeComprador.trim()) next.nomeComprador = 'Nome é obrigatório.';
    const digits = form.documentoFiscal.replace(/\D/g, '');
    if (!digits) next.documentoFiscal = 'Documento é obrigatório.';
    else if (digits.length !== 11 && digits.length !== 14)
      next.documentoFiscal = 'CPF deve ter 11 dígitos ou CNPJ 14 dígitos.';
    if (!form.idtEstado) next.idtEstado = 'Estado é obrigatório.';
    if (!form.idtCidade) next.idtCidade = 'Cidade é obrigatória.';
    setErrors(next);
    return Object.keys(next).length === 0;
  }

  function handleDocumento(raw: string) {
    const digits = raw.replace(/\D/g, '').slice(0, 14);
    setForm((prev) => ({ ...prev, documentoFiscal: digits }));
  }

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    if (!validate()) return;
    await onSubmit({
      nomeComprador: form.nomeComprador.trim(),
      documentoFiscal: form.documentoFiscal.replace(/\D/g, ''),
      idtCidade: form.idtCidade,
    });
  }

  const cidadePlaceholder = !form.idtEstado
    ? 'Selecione um estado primeiro'
    : loadingCidades
      ? 'Carregando cidades…'
      : cidadeOptions.length === 0
        ? 'Nenhuma cidade cadastrada para este estado'
        : 'Selecione a cidade';

  return (
    <form onSubmit={handleSubmit} noValidate>
      <div className="modal-body">
        <Input
          label="Nome"
          required
          value={form.nomeComprador}
          onChange={(e) => setForm((prev) => ({ ...prev, nomeComprador: e.target.value }))}
          error={errors.nomeComprador}
          placeholder="Nome completo ou razão social"
          autoFocus
        />
        <Input
          label="Documento (CPF / CNPJ)"
          required
          value={formatDocument(form.documentoFiscal)}
          onChange={(e) => handleDocumento(e.target.value)}
          error={errors.documentoFiscal}
          placeholder="000.000.000-00"
          inputMode="numeric"
        />
        <Select
          label="Estado"
          required
          options={estadoOptions}
          value={form.idtEstado}
          onChange={handleEstado}
          placeholder={loadingEstados ? 'Carregando estados…' : 'Selecione o estado'}
          error={errors.idtEstado}
          disabled={loadingEstados}
        />
        <Select
          label="Cidade"
          required
          options={cidadeOptions}
          value={form.idtCidade}
          onChange={(v) => setForm((prev) => ({ ...prev, idtCidade: v }))}
          placeholder={cidadePlaceholder}
          error={errors.idtCidade}
          disabled={!form.idtEstado || loadingCidades}
        />
      </div>
      <div className="modal-footer">
        <Button type="button" variant="secondary" onClick={onCancel} disabled={loading}>
          Cancelar
        </Button>
        <Button type="submit" loading={loading}>
          {initial ? 'Salvar alterações' : 'Cadastrar'}
        </Button>
      </div>
    </form>
  );
}
