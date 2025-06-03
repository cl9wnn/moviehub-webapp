import React from "react";
import { Link } from "react-router-dom";

interface RedirectMessageProps {
  message?: string;
  linkText: string;
  linkTo: string;
}

const RedirectMessage:React.FC<RedirectMessageProps> = ({message, linkText, linkTo}) => {
  return (
    <p className="text-sm text-center mt-4">
      {message}{" "}
      <Link to={linkTo} className="text-blue-600 hover:underline">
        {linkText}
      </Link>
    </p>
  );
};

export default RedirectMessage;