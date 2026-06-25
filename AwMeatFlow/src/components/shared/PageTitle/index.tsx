import type { ReactNode } from 'react';

interface PageTitleProps {
  title: string;
  actions?: ReactNode;
}

export function PageTitle({ title, actions }: PageTitleProps) {
  return (
    <div className="page-title-wrapper">
      <h1 className="page-title">{title}</h1>
      {actions && <div>{actions}</div>}
    </div>
  );
}
