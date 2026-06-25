import type { SelectHTMLAttributes } from 'react';

export interface SelectOption {
  value: string;
  label: string;
}

interface SelectProps extends Omit<SelectHTMLAttributes<HTMLSelectElement>, 'onChange'> {
  label?: string;
  error?: string;
  required?: boolean;
  options: SelectOption[];
  placeholder?: string;
  onChange?: (value: string) => void;
}

export function Select({
  label,
  error,
  required,
  options,
  placeholder,
  id,
  className = '',
  onChange,
  ...rest
}: SelectProps) {
  const selectId = id ?? label?.toLowerCase().replace(/\s+/g, '-');
  const classes = ['form-control', error ? 'error' : '', className].filter(Boolean).join(' ');

  return (
    <div className="form-group">
      {label && (
        <label className={`form-label${required ? ' required' : ''}`} htmlFor={selectId}>
          {label}
        </label>
      )}
      <select
        id={selectId}
        className={classes}
        required={required}
        onChange={(e) => onChange?.(e.target.value)}
        {...rest}
      >
        {placeholder && (
          <option value="" disabled>
            {placeholder}
          </option>
        )}
        {options.map((opt) => (
          <option key={opt.value} value={opt.value}>
            {opt.label}
          </option>
        ))}
      </select>
      {error && <span className="form-error">{error}</span>}
    </div>
  );
}
