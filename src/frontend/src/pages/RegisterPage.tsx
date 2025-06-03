import React, { useState } from "react";
import FormWrapper from "../components/auth/FormWrapper.tsx";
import InputField from "../components/common/InputField";
import Button from "../components/auth/Button.tsx";
import RedirectMessage from "../components/auth/RedirectMessage.tsx";
import {validateRegister} from "../utils/validation/authValidation.ts";
import {type RegisterRequest, registerUser} from "../services/auth/registerUser.ts";
import {useNavigate} from "react-router-dom";
import {useAuth} from "../hooks/UseAuth.tsx";

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

    const requestData:RegisterRequest = {
      username: formData.name,
      email: formData.email,
      password: formData.password,
    };

    try {
      const registerResult = await registerUser(requestData);
      localStorage.setItem("accessToken", registerResult.token);
      setIsAuthenticated(true);
      navigate("/personalize", { state: { successMessage: "Account created successfully!" } });
    } catch (err) {
      if (err instanceof Error) {
        setGlobalError(err.message);
      } else {
        setGlobalError("Something went wrong. Please try again.");
      }
    }
  };

  return (
    <FormWrapper title="Create account" stepLabel="Шаг 1" topPaddingClass="pt-32">
      <form onSubmit={handleSubmit} className="space-y-4" autoComplete="off">

        <InputField label="Username" name="name" value={formData.name} onChange={handleChange} placeholder="Enter your unique username" error={errors.name} />
        <InputField label="Email" name="email" value={formData.email} onChange={handleChange} type="email" placeholder="Enter your email address" error={errors.email} />
        <InputField label="Password" name="password" value={formData.password} onChange={handleChange} type="password" placeholder="Enter your password" error={errors.password} />

        <Button type="submit">Create your Account</Button>
      </form>

      {globalError && (
        <p className="text-sm text-red-600 text-center mt-2">{globalError}</p>
      )}

      <RedirectMessage message="Already have an account?" linkTo="/login" linkText="Sign In"/>
    </FormWrapper>
  );
};

export default RegisterPage;