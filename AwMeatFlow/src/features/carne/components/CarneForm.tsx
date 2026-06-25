import { useState } from 'react';
import { Input } from '../../../components/ui/Input';
import { Select } from '../../../components/ui/Select';
import { Button } from '../../../components/ui/Button';
import { ORIGENS_CARNE } from '../types';
import type { CarneResponseDTO } from '../dto/CarneResponseDTO';
import type { CreateCarneRequestDTO } from '../dto/CreateCarneRequestDTO';
import type { UpdateCarneRequestDTO } from '../dto/UpdateCarneRequestDTO';

interface CarneFormProps {
  initial?: CarneResponseDTO;
  onSubmit: (dto: CreateCarneRequestDTO | UpdateCarneRequestDTO) => Promise<void>;
  onCancel: () => void;
  loading: boolean;
}

interface FormState {
  descricaoCarne: string;
  origemCarne: string;
}

interface FormErrors {
  descricaoCarne?: string;
  origemCarne?: string;
}

const origemOptions = ORIGENS_CARNE.map((o) => ({ value: o, label: o }));

export function CarneForm({ initial, onSubmit, onCancel, loading }: CarneFormProps) {
  const [form, setForm] = useState<FormState>({
    descricaoCarne: initial?.descricaoCarne ?? '',
    origemCarne: initial?.origemCarne ?? '',
  });

  const [errors, setErrors] = useState<FormErrors>({});

  function validate(): boolean {
    const next: FormErrors = {};
    if (!form.descricaoCarne.trim()) next.descricaoCarne = 'Descrição é obrigatória.';
    if (!form.origemCarne) next.origemCarne = 'Origem é obrigatória.';
    setErrors(next);
    return Object.keys(next).length === 0;
  }

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    if (!validate()) return;
    await onSubmit({ descricaoCarne: form.descricaoCarne.trim(), origemCarne: form.origemCarne });
  }

  return (
    <form onSubmit={handleSubmit} noValidate>
      <div className="modal-body">
        <Input
          label="Descrição"
          required
          value={form.descricaoCarne}
          onChange={(e) => setForm((prev) => ({ ...prev, descricaoCarne: e.target.value }))}
          error={errors.descricaoCarne}
          placeholder="Ex: Picanha"
          autoFocus
        />
        <Select
          label="Origem"
          required
          options={origemOptions}
          value={form.origemCarne}
          onChange={(v) => setForm((prev) => ({ ...prev, origemCarne: v }))}
          placeholder="Selecione a origem"
          error={errors.origemCarne}
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
