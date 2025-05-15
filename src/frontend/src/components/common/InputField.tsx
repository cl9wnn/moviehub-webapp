import React from "react";

interface InputFieldProps {
  label: string;
  type?: string;
  value: string;
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
  name: string;
  placeholder?: string;
  error?: string;
}

const InputField: React.FC<InputFieldProps> = ({ label, type = "text", value, onChange, name, placeholder, error }) => (
  <div>
    <label className="block text-sm font-medium mb-1">{label}</label>
    <input
      name={name}
      type={type}
      value={value}
      onChange={onChange}
      placeholder={placeholder}
      className={`w-full border px-3 py-2 rounded ${
        error ? "border-red-500" : "border-gray-500"}`}
    />
    {error && <p className="text-sm text-red-500 mt-1">{error}</p>}
  </div>
);

export default InputField;