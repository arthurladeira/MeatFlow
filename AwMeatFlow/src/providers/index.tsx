import type { ReactNode } from 'react';
import { ToastProvider } from './ToastContext';
import { ToastContainer } from '../components/ui/Toast';

interface AppProvidersProps {
  children: ReactNode;
}

export function AppProviders({ children }: AppProvidersProps) {
  return (
    <ToastProvider>
      {children}
      <ToastContainer />
    </ToastProvider>
  );
}
