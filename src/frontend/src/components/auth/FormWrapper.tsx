import React from "react";

interface Props {
  title: string;
  children: React.ReactNode;
}

const FormWrapper: React.FC<Props> = ({ title, children }) => (
  <div className="min-h-screen flex items-center justify-center bg-gray-50 pt-20">
    <div className="bg-white p-6 rounded border border-gray-300 w-full max-w-md">
      <div className="flex justify-center mb-4">
        <div className="bg-yellow-500 text-black px-4 py-2 font-bold rounded">MovieHub</div>
      </div>
      <h2 className="text-xl font-semibold mb-4 text-center">{title}</h2>
      {children}
    </div>
  </div>
);

export default FormWrapper;