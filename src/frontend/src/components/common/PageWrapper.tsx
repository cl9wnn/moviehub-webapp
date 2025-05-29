import React from "react";

interface PageWrapperProps {
  children: React.ReactNode;
}

const PageWrapper: React.FC<PageWrapperProps> = ({ children }) => {
  return (
    <div className="max-w-6xl mx-auto p-6 mt-16 bg-white">
      {children}
    </div>
  );
};

export default PageWrapper;