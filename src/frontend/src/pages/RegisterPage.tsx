import React, { useState } from "react";
import FormWrapper from "../components/auth/FormWrapper.tsx";
import InputField from "../components/common/InputField";
import Button from "../components/auth/Button.tsx";
import RedirectMessage from "../components/auth/RedirectMessage.tsx";
import { validateRegister } from "../utils/validation/authValidation.ts";
import { type RegisterRequest, registerUser } from "../services/auth/registerUser.ts";
import { Link, useNavigate } from "react-router-dom";
import { useAuth } from "../hooks/UseAuth.tsx";

interface RegisterFormData {
  name: string;
  email: string;
  password: string;
}

const RegisterPage: React.FC = () => {
  const [formData, setFormData] = useState<RegisterFormData>({
    name: "",
    email: "",
    password: "",
  });

  const navigate = useNavigate();
  const [errors, setErrors] = useState<Partial<RegisterFormData>>({});
  const [globalError, setGlobalError] = useState<string | null>(null);
  const { setIsAuthenticated } = useAuth();

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setGlobalError(null);

    const validationErrors = validateRegister(formData);
    setErrors(validationErrors);

    if (Object.keys(validationErrors).length > 0) return;

    const requestData: RegisterRequest = {
      username: formData.name,
      email: formData.email,
      password: formData.password,
    };

    try {
      const registerResult = await registerUser(requestData);
      localStorage.setItem("accessToken", registerResult.token);
      setIsAuthenticated(true);
      navigate("/personalize", {
        state: { successMessage: "Аккаунт успешно создан!" },
      });
    } catch (err) {
      if (err instanceof Error) {
        setGlobalError(err.message);
      } else {
        setGlobalError("Что-то пошло не так. Попробуйте снова.");
      }
    }
  };

  return (
    <FormWrapper title="Создать аккаунт" stepLabel="Шаг 1" topPaddingClass="pt-32">
      <form onSubmit={handleSubmit} className="space-y-6" autoComplete="off">
        <InputField
          label="Никнейм"
          name="name"
          value={formData.name}
          onChange={handleChange}
          placeholder="Введите никнейм"
          error={errors.name}
        />
        <InputField
          label="Email"
          name="email"
          type="email"
          value={formData.email}
          onChange={handleChange}
          placeholder="Введите email"
          error={errors.email}
        />
        <InputField
          label="Пароль"
          name="password"
          type="password"
          value={formData.password}
          onChange={handleChange}
          placeholder="Введите пароль"
          error={errors.password}
        />

        {globalError && (
          <p className="text-sm text-red-600 text-center">{globalError}</p>
        )}

        <div className="mt-6">
          <Button type="submit">Создать аккаунт</Button>
        </div>
      </form>

      <div className="mt-6">
        <RedirectMessage
          message="Уже есть аккаунт?"
          linkTo="/login"
          linkText="Войти"
        />

        <p className="text-center text-sm mt-2">
          <Link to="/" className="text-blue-800 hover:underline">
            На главную
          </Link>
        </p>
      </div>
    </FormWrapper>
  );
};

export default RegisterPage;
