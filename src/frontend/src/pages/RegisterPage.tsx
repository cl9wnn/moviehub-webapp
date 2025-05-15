import React, { useState } from "react";
import FormWrapper from "../components/auth/FormWrapper.tsx";
import InputField from "../components/common/InputField";
import Button from "../components/auth/Button.tsx";
import RedirectMessage from "../components/auth/RedirectMessage.tsx";
import {validateRegister} from "../utils/validation/authValidation.ts";

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

  const [errors, setErrors] = useState<Partial<RegisterFormData>>({});

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    const validationErrors = validateRegister(formData);
    setErrors(validationErrors);

    if (Object.keys(validationErrors).length === 0) {
      console.log("Регистрация", formData);
    }
  };

  return (
    <FormWrapper title="Create account">
      <form onSubmit={handleSubmit} className="space-y-4" autoComplete="off">

        <InputField label="Username" name="name" value={formData.name} onChange={handleChange} placeholder="Enter your unique username" error={errors.name} />
        <InputField label="Email" name="email" value={formData.email} onChange={handleChange} type="email" placeholder="Enter your email address" error={errors.email} />
        <InputField label="Password" name="password" value={formData.password} onChange={handleChange} type="password" placeholder="Enter your password" error={errors.password} />

        <Button type="submit">Create your Account</Button>
      </form>

      <RedirectMessage message="Already have an account?" linkTo="/login" linkText="Sign In"/>
    </FormWrapper>
  );
};

export default RegisterPage;