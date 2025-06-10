import React from "react";

interface FormWrapperProps {
  title?: string;
  stepLabel?: string;
  children: React.ReactNode;
  topPaddingClass?: string;
  containerMaxWidthClass?: string;
}

const FormWrapper: React.FC<FormWrapperProps> = ({
                                                   title,
                                                   stepLabel,
                                                   children,
                                                   topPaddingClass = "pt-16 md:pt-20",
                                                   containerMaxWidthClass = "max-w-md"
                                                 }) => (
  <div className={`min-h-screen flex flex-col items-center bg-gray-450 ${topPaddingClass} px-4`}>
    <div className={`w-full ${containerMaxWidthClass}`}>
      {stepLabel && (
        <div className="mb-6 text-center">
          <span className="inline-block px-6 py-3 text-sm font-medium text-xl text-blue-600 bg-blue-50 rounded-full">
            {stepLabel}
          </span>
        </div>
      )}

      <div className="bg-white p-6 md:p-8 rounded-3xl">
        {title && (
          <h2 className="text-2xl font-bold text-gray-800 mb-6 text-center">
            {title}
          </h2>
        )}

        <div className="space-y-6">
          {children}
        </div>
      </div>
    </div>
  </div>
);

export default FormWrapper;
