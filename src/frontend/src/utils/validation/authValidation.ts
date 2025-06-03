export interface AuthFormData {
  name?: string;
  email?: string;
  password: string;
}

export interface AuthFormErrors {
  name?: string;
  email?: string;
  password?: string;
}

// Имя пользователя
function validateUsername(name?: string): string | undefined {
  if (!name?.trim()) return "Имя пользователя обязательно для заполнения";
  if (!/^[a-zA-Z0-9_\s]{5,20}$/.test(name))
    return "Имя пользователя должно содержать от 5 до 20 символов: буквы, цифры, пробелы или подчёркивания.";
}

// Email
function validateEmail(email?: string): string | undefined {
  if (!email?.trim()) return "Email обязателен для заполнения";
  if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email))
    return "Неверный формат email-адреса.";
}

// Пароль
function validatePassword(password?: string): string | undefined {
  if (!password?.trim()) return "Пароль обязателен для заполнения";
  if (!/^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$/.test(password))
    return "Пароль должен содержать от 8 до 20 символов, включать букву, цифру и специальный символ.";
}

export function validateRegister(data: AuthFormData): AuthFormErrors {
  const errors: AuthFormErrors = {};

  const nameError = validateUsername(data.name);
  const emailError = validateEmail(data.email);
  const passwordError = validatePassword(data.password);

  if (nameError) errors.name = nameError;
  if (emailError) errors.email = emailError;
  if (passwordError) errors.password = passwordError;

  return errors;
}

export function validateLogin(data: AuthFormData): AuthFormErrors {
  const errors: AuthFormErrors = {};

  const nameError = validateUsername(data.name);
  const passwordError = validatePassword(data.password);

  if (nameError) errors.name = nameError;
  if (passwordError) errors.password = passwordError;

  return errors;
}
