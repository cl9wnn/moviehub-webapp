import React from "react";
import clsx from "clsx";

interface AuthButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement>{
  children:React.ReactNode;
  fullWidth?: boolean;
}

const Button: React.FC<AuthButtonProps> = ({children, fullWidth = true, className, ...props}) => {
  return (
    <button
      {...props}
      className = {clsx(
        "w-full px-4 py-3.5 rounded-xl bg-yellow-500 text-gray-800 font-semibold hover:bg-yellow-550",
      fullWidth && "w-full",
      className
    )}>
      {children}
    </button>
  );
};

export default Button;