interface EmptyStateProps {
  message?: string;
  icon?: string;
}

export function EmptyState({
  message = 'Nenhum registro encontrado.',
  icon = '📭',
}: EmptyStateProps) {
  return (
    <div className="empty-state">
      <span className="empty-state-icon" role="img" aria-hidden="true">
        {icon}
      </span>
      <p className="empty-state-message">{message}</p>
    </div>
  );
}
