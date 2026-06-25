import { Outlet } from 'react-router-dom';
import { Header } from '../components/shared/Header';

export function AppLayout() {
  return (
    <>
      <Header />
      <main className="main-content">
        <div className="container">
          <Outlet />
        </div>
      </main>
    </>
  );
}
