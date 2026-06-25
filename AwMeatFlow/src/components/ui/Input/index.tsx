import type { InputHTMLAttributes } from 'react';

interface InputProps extends InputHTMLAttributes<HTMLInputElement> {
  label?: string;
  error?: string;
  required?: boolean;
}

export function Input({ label, error, required, id, className = '', ...rest }: InputProps) {
  const inputId = id ?? label?.toLowerCase().replace(/\s+/g, '-');
  const classes = ['form-control', error ? 'error' : '', className].filter(Boolean).join(' ');

  return (
    <div className="form-group">
      {label && (
        <label className={`form-label${required ? ' required' : ''}`} htmlFor={inputId}>
          {label}
        </label>
      )}
      <input id={inputId} className={classes} required={required} {...rest} />
      {error && <span className="form-error">{error}</span>}
    </div>
  );
}
