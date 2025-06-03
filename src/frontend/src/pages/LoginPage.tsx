import React, {useEffect, useState} from "react";
import FormWrapper from "../components/auth/FormWrapper.tsx";
import InputField from "../components/common/InputField";
import Button from "../components/auth/Button.tsx";
import RedirectMessage from "../components/auth/RedirectMessage.tsx";
import {validateLogin} from "../utils/validation/authValidation.ts";
import {useLocation, useNavigate} from "react-router-dom";
import  {type LoginRequest, loginUser} from "../services/auth/loginUser.ts";
import {toast} from "react-toastify";
import {useAuth} from "../hooks/UseAuth";

export interface LoginFormData {
  name: string;
  password: string;
}

const LoginPage: React.FC = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const { setIsAuthenticated } = useAuth();
  const from = location.state?.from?.pathname || "/";

  useEffect(() => {
    const successMessage = location.state?.successMessage;
    const errorMessage = location.state?.errorMessage;

    if (successMessage) {
      toast.success(successMessage);
    }

    if (errorMessage){
      toast.error(errorMessage);
    }
  }, [location, navigate]);

  const [formData, setFormData] = useState<LoginFormData>({
    name: "",
    password: "",
  });

  const [errors, setErrors] = useState<Partial<LoginFormData>>({});
  const [globalError, setGlobalError] = useState<string | null>(null);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setGlobalError(null);

    const validationErrors = validateLogin(formData);
    setErrors(validationErrors);

    if (Object.keys(validationErrors).length > 0) return;

    const requestData:LoginRequest = {
      username: formData.name,
      password: formData.password,
    };

    try {
      const loginData = await loginUser(requestData);
      localStorage.setItem("accessToken", loginData.token);
      setIsAuthenticated(true);
      navigate(from, { state: { successMessage: "Successful authentication!" }, replace: true });
    } catch (err) {
      if (err instanceof Error) {
        setGlobalError(err.message);
      } else {
        setGlobalError("Something went wrong. Please try again.");
      }
    }

  };

  return (
    <FormWrapper title="Sign in" topPaddingClass="pt-48">
      <form onSubmit={handleSubmit} className="space-y-4">

        <InputField label="Username" name="name" value={formData.name} onChange={handleChange} placeholder="Enter your username" error={errors.name}/>
        <InputField label="Password" name="password" value={formData.password} onChange={handleChange} type="password" placeholder="Enter your password" error={errors.password}/>

        <Button type="submit">Sign In</Button>
      </form>

      {globalError && (
        <p className="text-sm text-red-600 text-center mt-2">{globalError}</p>
      )}

     <RedirectMessage message="New To MovieHub?" linkTo="/register" linkText="Sign up"/>
    </FormWrapper>
  );
};

export default LoginPage;