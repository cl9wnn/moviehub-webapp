import React from "react";

interface FormWrapperProps {
  title?: string;
  stepLabel?: string;
  children: React.ReactNode;
  topPaddingClass?: string;
}

const FormWrapper: React.FC<FormWrapperProps> = ({ title, stepLabel, children, topPaddingClass = "pt-20" }) => (
  <div className={`min-h-screen flex flex-col items-center bg-gray-50 ${topPaddingClass} px-4`}>
    {stepLabel && (
      <h3 className="text-2xl font-bold text-center mb-4">{stepLabel}</h3>
    )}
    <div className="bg-white p-6 rounded border border-gray-300 w-full max-w-md shadow">
      <div className="flex justify-center mb-4">
        <div className="bg-yellow-500 text-black px-4 py-2 font-bold rounded">MovieHub</div>
      </div>
      {title && (
        <h2 className="text-xl font-semibold mb-4 text-center">{title}</h2>
      )}

      {children}
    </div>
  </div>
);

export default FormWrapper;