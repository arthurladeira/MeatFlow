import { type ReactNode, useState, useRef, useCallback, useEffect } from 'react';

export interface TableColumn<T> {
  key: string;
  header: string;
  render: (row: T) => ReactNode;
  width?: number;
}

interface DataTableProps<T> {
  columns: TableColumn<T>[];
  data: T[];
  keyExtractor: (row: T) => string;
}

export function DataTable<T>({ columns, data, keyExtractor }: DataTableProps<T>) {
  const [widths, setWidths] = useState<(number | null)[]>(() =>
    columns.map((col) => col.width ?? null)
  );

  const dragging = useRef<{ colIndex: number; startX: number; startWidth: number } | null>(null);

  const onResizeStart = useCallback((e: React.MouseEvent, colIndex: number) => {
    e.preventDefault();
    const th = (e.currentTarget as HTMLElement).parentElement!;
    dragging.current = {
      colIndex,
      startX: e.clientX,
      startWidth: th.getBoundingClientRect().width,
    };
  }, []);

  useEffect(() => {
    function onMouseMove(e: MouseEvent) {
      if (!dragging.current) return;
      const { colIndex, startX, startWidth } = dragging.current;
      const newWidth = Math.max(50, startWidth + (e.clientX - startX));
      setWidths((prev) => {
        const next = [...prev];
        next[colIndex] = newWidth;
        return next;
      });
    }

    function onMouseUp() {
      dragging.current = null;
    }

    document.addEventListener('mousemove', onMouseMove);
    document.addEventListener('mouseup', onMouseUp);
    return () => {
      document.removeEventListener('mousemove', onMouseMove);
      document.removeEventListener('mouseup', onMouseUp);
    };
  }, []);

  return (
    <div className="table-wrapper">
      <table className="table">
        <thead>
          <tr>
            {columns.map((col, i) => (
              <th
                key={col.key}
                style={widths[i] != null ? { width: widths[i], minWidth: widths[i] } : undefined}
              >
                {col.header}
                <div
                  className="th-resize-handle"
                  onMouseDown={(e) => onResizeStart(e, i)}
                />
              </th>
            ))}
          </tr>
        </thead>
        <tbody>
          {data.map((row) => (
            <tr key={keyExtractor(row)}>
              {columns.map((col) => (
                <td key={col.key}>{col.render(row)}</td>
              ))}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
