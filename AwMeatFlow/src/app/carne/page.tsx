import { useState } from 'react';
import { PageTitle } from '../../components/shared/PageTitle';
import { ConfirmDialog } from '../../components/shared/ConfirmDialog';
import { Modal } from '../../components/ui/Modal';
import { Button } from '../../components/ui/Button';
import { Loading } from '../../components/ui/Loading';
import { EmptyState } from '../../components/ui/EmptyState';
import { CarneTable } from '../../features/carne/components/CarneTable';
import { CarneForm } from '../../features/carne/components/CarneForm';
import { useCarnes } from '../../features/carne/hooks/useCarnes';
import { useCarneById } from '../../features/carne/hooks/useCarneById';
import { useCreateCarne } from '../../features/carne/hooks/useCreateCarne';
import { useUpdateCarne } from '../../features/carne/hooks/useUpdateCarne';
import { useDeleteCarne } from '../../features/carne/hooks/useDeleteCarne';
import { useToast } from '../../providers/ToastContext';
import type { CarneResponseDTO } from '../../features/carne/dto/CarneResponseDTO';
import type { CreateCarneRequestDTO } from '../../features/carne/dto/CreateCarneRequestDTO';
import type { UpdateCarneRequestDTO } from '../../features/carne/dto/UpdateCarneRequestDTO';

export function CarnePage() {
  const { carnes, loading, error, refetch } = useCarnes();
  const { fetch: fetchById } = useCarneById();
  const { mutate: create, loading: creating } = useCreateCarne();
  const { mutate: update, loading: updating } = useUpdateCarne();
  const { mutate: remove, loading: deleting, error: deleteError } = useDeleteCarne();
  const { showToast } = useToast();

  const [editing, setEditing] = useState<CarneResponseDTO | null>(null);
  const [showForm, setShowForm] = useState(false);
  const [toDelete, setToDelete] = useState<CarneResponseDTO | null>(null);

  function openCreate() {
    setEditing(null);
    setShowForm(true);
  }

  async function openEdit(carne: CarneResponseDTO) {
    const data = await fetchById(carne.idtCarne);
    if (data) {
      setEditing(data);
      setShowForm(true);
    }
  }

  function closeForm() {
    setShowForm(false);
    setEditing(null);
  }

  async function handleSubmit(dto: CreateCarneRequestDTO | UpdateCarneRequestDTO) {
    const isEditing = !!editing;
    const result = isEditing
      ? await update(editing!.idtCarne, dto as UpdateCarneRequestDTO)
      : await create(dto as CreateCarneRequestDTO);

    if (result) {
      closeForm();
      await refetch();
      showToast(isEditing ? 'Carne atualizada com sucesso.' : 'Carne cadastrada com sucesso.', 'success');
    } else {
      showToast(isEditing ? 'Erro ao atualizar carne.' : 'Erro ao cadastrar carne.', 'error');
    }
  }

  async function handleDelete() {
    if (!toDelete) return;
    const { success, errorMessage } = await remove(toDelete.idtCarne);
    setToDelete(null);
    if (success) {
      await refetch();
      showToast('Carne excluída com sucesso.', 'success');
    } else {
      showToast(errorMessage ?? 'Erro ao excluir carne.', 'error');
    }
  }

  const mutating = creating || updating;

  return (
    <>
      <PageTitle
        title="Carnes"
        actions={
          <Button onClick={openCreate}>+ Nova carne</Button>
        }
      />

      {error && <div className="error-message">{error}</div>}

      <div className="card">
        {loading ? (
          <Loading />
        ) : carnes.length === 0 ? (
          <EmptyState message="Nenhuma carne cadastrada." icon="🥩" />
        ) : (
          <CarneTable carnes={carnes} onEdit={openEdit} onDelete={setToDelete} />
        )}
      </div>

      <Modal
        open={showForm}
        onClose={closeForm}
        title={editing ? 'Editar carne' : 'Nova carne'}
        size="md"
      >
        <CarneForm
          initial={editing ?? undefined}
          onSubmit={handleSubmit}
          onCancel={closeForm}
          loading={mutating}
        />
      </Modal>

      <ConfirmDialog
        open={Boolean(toDelete)}
        title="Excluir carne"
        message={
          deleteError
            ? deleteError
            : `Deseja excluir "${toDelete?.descricaoCarne}"? Esta ação não pode ser desfeita.`
        }
        confirmLabel="Excluir"
        onConfirm={handleDelete}
        onCancel={() => setToDelete(null)}
        loading={deleting}
      />
    </>
  );
}
