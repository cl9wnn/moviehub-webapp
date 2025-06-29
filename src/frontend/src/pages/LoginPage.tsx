import React, { useEffect, useState } from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

import FormWrapper from "../components/auth/FormWrapper.tsx";
import InputField from "../components/common/InputField";
import Button from "../components/auth/Button.tsx";
import RedirectMessage from "../components/auth/RedirectMessage.tsx";
import { validateLogin } from "../utils/validation/authValidation.ts";
import { type LoginRequest, loginUser } from "../services/auth/loginUser.ts";
import { useAuth } from "../hooks/UseAuth";
import { getCurrentUserId } from "../hooks/useCurrentUserId";

export interface LoginFormData {
  name: string;
  password: string;
}

const LoginPage: React.FC = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const { setIsAuthenticated } = useAuth();

  const [formData, setFormData] = useState<LoginFormData>({
    name: "",
    password: "",
  });

  const [errors, setErrors] = useState<Partial<LoginFormData>>({});
  const [globalError, setGlobalError] = useState<string | null>(null);

  useEffect(() => {
    const successMessage = location.state?.successMessage;
    const errorMessage = location.state?.errorMessage;

    if (successMessage) toast.success(successMessage);
    if (errorMessage) toast.error(errorMessage);
  }, [location]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setGlobalError(null);

    const validationErrors = validateLogin(formData);
    setErrors(validationErrors);
    if (Object.keys(validationErrors).length > 0) return;

    const requestData: LoginRequest = {
      username: formData.name,
      password: formData.password,
    };

    try {
      const loginData = await loginUser(requestData);
      localStorage.setItem("accessToken", loginData.token);
      setIsAuthenticated(true);
      const userId = getCurrentUserId();
      if (userId) {
        navigate(`/users/${userId}`, {
          replace: true,
          state: { successMessage: "Успешный вход!" },
        });
      } else {
        navigate("/", { replace: true });
      }    } catch (err) {
      setGlobalError(err instanceof Error ? err.message : "Что-то пошло не так. Попробуйте снова.");
    }
  };

  return (
    <FormWrapper title="Войти" topPaddingClass="pt-48">
      <form onSubmit={handleSubmit} className="space-y-6" autoComplete="off">
        <InputField
          label="Username"
          name="name"
          value={formData.name}
          onChange={handleChange}
          placeholder="Введите никнейм"
          error={errors.name}
        />
        <InputField
          label="Password"
          name="password"
          value={formData.password}
          onChange={handleChange}
          type="password"
          placeholder="Введите пароль"
          error={errors.password}
        />

        <div className="mt-6">
          <Button type="submit">Войти</Button>
        </div>

        {globalError && (
          <p className="text-sm text-red-600 text-center">{globalError}</p>
        )}
      </form>

      <div className="mt-3">
        <RedirectMessage
          message="Впервые на сайте?"
          linkTo="/register"
          linkText="Зарегистрироваться"
        />
      </div>

      <p className="text-center text-sm mt-0">
        <Link to="/" className="text-blue-800 hover:underline">
          На главную
        </Link>
      </p>
    </FormWrapper>
  );
};

export default LoginPage;
