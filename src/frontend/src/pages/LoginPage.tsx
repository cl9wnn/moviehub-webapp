import React, { useState } from "react";
import FormWrapper from "../components/auth/FormWrapper.tsx";
import InputField from "../components/common/InputField";
import Button from "../components/auth/Button.tsx";
import RedirectMessage from "../components/auth/RedirectMessage.tsx";
import {validateLogin} from "../utils/validation/authValidation.ts";

export interface LoginFormData {
  username: string;
  password: string;
}

const LoginPage: React.FC = () => {
  const [formData, setFormData] = useState<LoginFormData>({
    username: "",
    password: "",
  });

  const [errors, setErrors] = useState<Partial<RegisterFormData>>({});

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: value }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    const validationErrors = validateLogin(formData);
    setErrors(validationErrors);

    if (Object.keys(validationErrors).length === 0) {
      console.log("Вход", formData);
    }
  };

  return (
    <FormWrapper title="Sign in">
      <form onSubmit={handleSubmit} className="space-y-4">

        <InputField label="Username" name="username" value={formData.username} onChange={handleChange} placeholder="Enter your username" error={errors.name}/>
        <InputField label="Password" name="password" value={formData.password} onChange={handleChange} type="password" placeholder="Enter your password" error={errors.password}/>

        <Button type="submit">Sign In</Button>
      </form>

     <RedirectMessage message="New To MovieHub?" linkTo="/register" linkText="Sign up"/>
    </FormWrapper>
  );
};

export default LoginPage;