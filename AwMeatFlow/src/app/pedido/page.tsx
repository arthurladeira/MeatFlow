import { useState, useEffect, useCallback } from 'react';
import { PageTitle } from '../../components/shared/PageTitle';
import { ConfirmDialog } from '../../components/shared/ConfirmDialog';
import { Modal } from '../../components/ui/Modal';
import { Button } from '../../components/ui/Button';
import { Loading } from '../../components/ui/Loading';
import { EmptyState } from '../../components/ui/EmptyState';
import { PedidoTable } from '../../features/pedido/components/PedidoTable';
import { PedidoForm } from '../../features/pedido/components/PedidoForm';
import { usePedidos } from '../../features/pedido/hooks/usePedidos';
import { usePedidoById } from '../../features/pedido/hooks/usePedidoById';
import { useCreatePedido } from '../../features/pedido/hooks/useCreatePedido';
import { useUpdatePedido } from '../../features/pedido/hooks/useUpdatePedido';
import { useDeletePedido } from '../../features/pedido/hooks/useDeletePedido';
import { useCotacoes } from '../../features/cotacao/hooks/useCotacoes';
import { getCompradores } from '../../features/comprador/services/getCompradores';
import { getCarnes } from '../../features/carne/services/getCarnes';
import type { PedidoResponseDTO } from '../../features/pedido/dto/PedidoResponseDTO';
import type { CreatePedidoRequestDTO } from '../../features/pedido/dto/CreatePedidoRequestDTO';
import type { UpdatePedidoRequestDTO } from '../../features/pedido/dto/UpdatePedidoRequestDTO';
import type { CompradorResponseDTO } from '../../features/comprador/dto/CompradorResponseDTO';
import type { CarneResponseDTO } from '../../features/carne/dto/CarneResponseDTO';

export function PedidoPage() {
  const { pedidos, loading, error, refetch } = usePedidos();
  const { fetch: fetchById } = usePedidoById();
  const { mutate: create, loading: creating } = useCreatePedido();
  const { mutate: update, loading: updating } = useUpdatePedido();
  const { mutate: remove, loading: deleting, error: deleteError } = useDeletePedido();
  const { cotacoes, loading: cotacoesLoading } = useCotacoes();

  const [compradores, setCompradores] = useState<CompradorResponseDTO[]>([]);
  const [carnes, setCarnes] = useState<CarneResponseDTO[]>([]);

  const [editing, setEditing] = useState<PedidoResponseDTO | null>(null);
  const [showForm, setShowForm] = useState(false);
  const [toDelete, setToDelete] = useState<PedidoResponseDTO | null>(null);

  const loadDependencies = useCallback(async () => {
    const [c, k] = await Promise.all([getCompradores(), getCarnes()]);
    setCompradores(c);
    setCarnes(k);
  }, []);

  useEffect(() => {
    loadDependencies();
  }, [loadDependencies]);

  function openCreate() {
    setEditing(null);
    setShowForm(true);
  }

  async function openEdit(pedido: PedidoResponseDTO) {
    const data = await fetchById(pedido.idtPedido);
    if (data) {
      setEditing(data);
      setShowForm(true);
    }
  }

  function closeForm() {
    setShowForm(false);
    setEditing(null);
  }

  async function handleSubmit(dto: CreatePedidoRequestDTO | UpdatePedidoRequestDTO) {
    const result = editing
      ? await update(editing.idtPedido, dto as UpdatePedidoRequestDTO)
      : await create(dto as CreatePedidoRequestDTO);

    if (result) {
      closeForm();
      await refetch();
    }
  }

  async function handleDelete() {
    if (!toDelete) return;
    const success = await remove(toDelete.idtPedido);
    if (success) {
      setToDelete(null);
      await refetch();
    }
  }

  const mutating = creating || updating;

  return (
    <>
      <PageTitle
        title="Pedidos"
        actions={<Button onClick={openCreate}>+ Novo pedido</Button>}
      />

      {error && <div className="error-message">{error}</div>}

      <div className="card">
        {loading ? (
          <Loading />
        ) : pedidos.length === 0 ? (
          <EmptyState message="Nenhum pedido registrado." icon="📦" />
        ) : (
          <PedidoTable
            pedidos={pedidos}
            onEdit={openEdit}
            onDelete={setToDelete}
            cotacoes={cotacoes}
            cotacoesLoading={cotacoesLoading}
          />
        )}
      </div>

      <Modal
        open={showForm}
        onClose={closeForm}
        title={editing ? 'Editar pedido' : 'Novo pedido'}
        size="xl"
      >
        <PedidoForm
          initial={editing ?? undefined}
          compradores={compradores}
          carnes={carnes}
          onSubmit={handleSubmit}
          onCancel={closeForm}
          loading={mutating}
          cotacoes={cotacoesLoading ? undefined : cotacoes}
        />
      </Modal>

      <ConfirmDialog
        open={Boolean(toDelete)}
        title="Excluir pedido"
        message={
          deleteError
            ? deleteError
            : `Deseja excluir o pedido de "${toDelete?.nomeComprador}"? Esta ação não pode ser desfeita.`
        }
        confirmLabel="Excluir"
        onConfirm={handleDelete}
        onCancel={() => setToDelete(null)}
        loading={deleting}
      />
    </>
  );
}
