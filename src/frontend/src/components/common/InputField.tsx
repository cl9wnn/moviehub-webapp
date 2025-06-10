import React, { useState } from "react";
import { FaEye, FaEyeSlash } from "react-icons/fa";

interface InputFieldProps {
  label?: string;
  type?: string;
  value: string;
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
  name: string;
  placeholder: string;
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
  const [isFocused, setIsFocused] = useState(false);

  const isPasswordField = type === "password";
  const inputType = isPasswordField && showPassword ? "text" : type;

  return (
    <div className="mb-5">
      <div className="relative">
        <input
          name={name}
          type={inputType}
          value={value}
          onChange={onChange}
          placeholder={placeholder}
          onFocus={() => setIsFocused(true)}
          onBlur={() => setIsFocused(false)}
          aria-label={label || placeholder}
          className={`w-full px-4 py-3.5 text-gray-700 bg-[#dce8fc] rounded-xl
          transition-all duration-200 focus:outline-none
          placeholder:text-gray-600 placeholder:font-base
          ${isFocused ? "ring-2 ring-blue-300 bg-blue-100" : ""}
          ${isPasswordField ? "pr-12" : ""}
          ${error ? "bg-red-50 ring-2 ring-red-300" : ""}`}
        />

        {isPasswordField && (
          <button
            type="button"
            className={`absolute right-4 top-1/2 transform -translate-y-1/2
            text-blue-400 hover:text-blue-600 transition-colors
            focus:outline-none rounded-full p-1 ${
              isFocused ? "text-blue-600" : ""
            }`}
            onClick={() => setShowPassword(!showPassword)}
            aria-label={showPassword ? "Скрыть пароль" : "Показать пароль"}
          >
            {showPassword ? <FaEyeSlash size={20} /> : <FaEye size={20} />}
          </button>
        )}
      </div>
      {error && (
        <p className="text-sm text-red-500 mt-2 ml-1 font-medium">{error}</p>
      )}
    </div>
  );
};

export default InputField;