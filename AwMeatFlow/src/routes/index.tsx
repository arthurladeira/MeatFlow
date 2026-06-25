import { createBrowserRouter, RouterProvider, Navigate } from 'react-router-dom';
import { AppLayout } from '../layouts/AppLayout';
import { CarnePage } from '../app/carne/page';
import { CompradorPage } from '../app/comprador/page';
import { PedidoPage } from '../app/pedido/page';

const router = createBrowserRouter([
  {
    path: '/',
    element: <AppLayout />,
    children: [
      { index: true, element: <Navigate to="/carne" replace /> },
      { path: 'carne', element: <CarnePage /> },
      { path: 'comprador', element: <CompradorPage /> },
      { path: 'pedido', element: <PedidoPage /> },
    ],
  },
]);

export function AppRoutes() {
  return <RouterProvider router={router} />;
}
