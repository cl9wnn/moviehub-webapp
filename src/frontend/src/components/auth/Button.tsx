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
        "bg-yellow-500 hover:bg-yellow-600 text-black font-semibold py-2 rounded transition-colors duration-200 !mt-8",
      fullWidth && "w-full",
      className
    )}>
      {children}
    </button>
  );
};

export default Button;