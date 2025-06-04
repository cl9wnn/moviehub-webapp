import React, { useState } from "react";
import { FaEye, FaEyeSlash } from "react-icons/fa";

interface InputFieldProps {
  label: string;
  type?: string;
  value: string;
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
  name: string;
  placeholder?: string;
  error?: string;
}

const InputField: React.FC<InputFieldProps> = ({
                                                 label,
                                                 type = "text",
                                                 value,
                                                 onChange,
                                                 name,
                                                 placeholder,
                                                 error,
                                               }) => {
  const [showPassword, setShowPassword] = useState(false);

  const isPasswordField = type === "password";
  const inputType = isPasswordField && showPassword ? "text" : type;

  return (
    <div>
      <label className="block text-sm font-medium mb-1">{label}</label>
      <div className="relative">
        <input
          name={name}
          type={inputType}
          value={value}
          onChange={onChange}
          placeholder={placeholder}
          className={`w-full border px-3 py-2 rounded ${
            error ? "border-red-500" : "border-gray-500"
          } ${isPasswordField ? "pr-10" : ""}`}
        />

        {isPasswordField && (
          <button
            type="button"
            className="absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-600 hover:text-gray-800 focus:outline-none"
            onClick={() => setShowPassword(!showPassword)}
            aria-label={showPassword ? "Скрыть пароль" : "Показать пароль"}
          >
            {showPassword ? <FaEyeSlash size={18} /> : <FaEye size={18} />}
          </button>
        )}
      </div>
      {error && <p className="text-sm text-red-500 mt-1">{error}</p>}
    </div>
  );
};

export default InputField;
