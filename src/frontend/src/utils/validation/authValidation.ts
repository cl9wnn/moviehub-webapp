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

// Username
function validateUsername(name?: string): string | undefined {
  if (!name?.trim()) return "Username is required";
  if (!/^[a-zA-Z0-9_\s]{5,20}$/.test(name))
    return "Username must be 5–20 characters: letters, numbers, underscores or spaces.";
}

// Email
function validateEmail(email?: string): string | undefined {
  if (!email?.trim()) return "Email is required";
  if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email))
    return "Invalid email address format.";
}

// Password
function validatePassword(password?: string): string | undefined {
  if (!password?.trim()) return "Password is required";
  if (!/^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$/.test(password))
    return "Password must be 8–20 characters, include a digit, a special symbol, and a letter.";
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
