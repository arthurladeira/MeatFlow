import { useState } from 'react';
import { PageTitle } from '../../components/shared/PageTitle';
import { ConfirmDialog } from '../../components/shared/ConfirmDialog';
import { Modal } from '../../components/ui/Modal';
import { Button } from '../../components/ui/Button';
import { Loading } from '../../components/ui/Loading';
import { EmptyState } from '../../components/ui/EmptyState';
import { CompradorTable } from '../../features/comprador/components/CompradorTable';
import { CompradorForm } from '../../features/comprador/components/CompradorForm';
import { useCompradores } from '../../features/comprador/hooks/useCompradores';
import { useCompradorById } from '../../features/comprador/hooks/useCompradorById';
import { useCreateComprador } from '../../features/comprador/hooks/useCreateComprador';
import { useUpdateComprador } from '../../features/comprador/hooks/useUpdateComprador';
import { useDeleteComprador } from '../../features/comprador/hooks/useDeleteComprador';
import { useToast } from '../../providers/ToastContext';
import type { CompradorResponseDTO } from '../../features/comprador/dto/CompradorResponseDTO';
import type { CreateCompradorRequestDTO } from '../../features/comprador/dto/CreateCompradorRequestDTO';
import type { UpdateCompradorRequestDTO } from '../../features/comprador/dto/UpdateCompradorRequestDTO';

export function CompradorPage() {
  const { compradores, loading, error, refetch } = useCompradores();
  const { fetch: fetchById } = useCompradorById();
  const { mutate: create, loading: creating } = useCreateComprador();
  const { mutate: update, loading: updating } = useUpdateComprador();
  const { mutate: remove, loading: deleting, error: deleteError } = useDeleteComprador();
  const { showToast } = useToast();

  const [editing, setEditing] = useState<CompradorResponseDTO | null>(null);
  const [showForm, setShowForm] = useState(false);
  const [toDelete, setToDelete] = useState<CompradorResponseDTO | null>(null);

  function openCreate() {
    setEditing(null);
    setShowForm(true);
  }

  async function openEdit(comprador: CompradorResponseDTO) {
    const data = await fetchById(comprador.idtComprador);
    if (data) {
      setEditing(data);
      setShowForm(true);
    }
  }

  function closeForm() {
    setShowForm(false);
    setEditing(null);
  }

  async function handleSubmit(dto: CreateCompradorRequestDTO | UpdateCompradorRequestDTO) {
    const isEditing = !!editing;
    const result = isEditing
      ? await update(editing!.idtComprador, dto as UpdateCompradorRequestDTO)
      : await create(dto as CreateCompradorRequestDTO);

    if (result) {
      closeForm();
      await refetch();
      showToast(isEditing ? 'Comprador atualizado com sucesso.' : 'Comprador cadastrado com sucesso.', 'success');
    } else {
      showToast(isEditing ? 'Erro ao atualizar comprador.' : 'Erro ao cadastrar comprador.', 'error');
    }
  }

  async function handleDelete() {
    if (!toDelete) return;
    const { success, errorMessage } = await remove(toDelete.idtComprador);
    setToDelete(null);
    if (success) {
      await refetch();
      showToast('Comprador excluído com sucesso.', 'success');
    } else {
      showToast(errorMessage ?? 'Erro ao excluir comprador.', 'error');
    }
  }

  const mutating = creating || updating;

  return (
    <>
      <PageTitle
        title="Compradores"
        actions={<Button onClick={openCreate}>+ Novo comprador</Button>}
      />

      {error && <div className="error-message">{error}</div>}

      <div className="card">
        {loading ? (
          <Loading />
        ) : compradores.length === 0 ? (
          <EmptyState message="Nenhum comprador cadastrado." icon="👤" />
        ) : (
          <CompradorTable
            compradores={compradores}
            onEdit={openEdit}
            onDelete={setToDelete}
          />
        )}
      </div>

      <Modal
        open={showForm}
        onClose={closeForm}
        title={editing ? 'Editar comprador' : 'Novo comprador'}
        size="md"
      >
        <CompradorForm
          initial={editing ?? undefined}
          onSubmit={handleSubmit}
          onCancel={closeForm}
          loading={mutating}
        />
      </Modal>

      <ConfirmDialog
        open={Boolean(toDelete)}
        title="Excluir comprador"
        message={
          deleteError
            ? deleteError
            : `Deseja excluir "${toDelete?.nomeComprador}"? Esta ação não pode ser desfeita.`
        }
        confirmLabel="Excluir"
        onConfirm={handleDelete}
        onCancel={() => setToDelete(null)}
        loading={deleting}
      />
    </>
  );
}
