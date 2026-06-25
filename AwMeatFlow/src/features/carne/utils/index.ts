import type { OrigemCarne } from '../types';

const BADGE_CLASS: Record<OrigemCarne, string> = {
  Bovina: 'badge badge-blue',
  'Suína': 'badge badge-orange',
  Aves: 'badge badge-green',
  Peixes: 'badge badge-purple',
};

export function getOrigemBadgeClass(origem: string): string {
  return BADGE_CLASS[origem as OrigemCarne] ?? 'badge badge-blue';
}
