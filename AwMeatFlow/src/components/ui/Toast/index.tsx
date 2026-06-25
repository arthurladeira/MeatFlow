import { useToast, type ToastItem } from '../../../providers/ToastContext';

function Toast({ id, message, type }: ToastItem) {
  const { removeToast } = useToast();

  return (
    <div className={`toast toast-${type}`}>
      <span className="toast-icon">{type === 'success' ? '✓' : '✕'}</span>
      <span className="toast-message">{message}</span>
      <button className="toast-close" onClick={() => removeToast(id)} aria-label="Fechar">
        ×
      </button>
    </div>
  );
}

export function ToastContainer() {
  const { toasts } = useToast();
  if (toasts.length === 0) return null;

  return (
    <div className="toast-container">
      {toasts.map((t) => (
        <Toast key={t.id} {...t} />
      ))}
    </div>
  );
}
