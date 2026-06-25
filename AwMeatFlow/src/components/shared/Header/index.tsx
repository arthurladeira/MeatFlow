import { NavLink } from 'react-router-dom';

const navItems = [
  { to: '/carne', label: 'Carnes' },
  { to: '/comprador', label: 'Compradores' },
  { to: '/pedido', label: 'Pedidos' },
] as const;

export function Header() {
  return (
    <header className="header">
      <div className="container header-inner">
        <NavLink to="/" className="header-brand">
          🥩 MeatFlow
        </NavLink>
        <nav className="header-nav" aria-label="Navegação principal">
          {navItems.map((item) => (
            <NavLink
              key={item.to}
              to={item.to}
              className={({ isActive }) => `nav-link${isActive ? ' active' : ''}`}
            >
              {item.label}
            </NavLink>
          ))}
        </nav>
      </div>
    </header>
  );
}
