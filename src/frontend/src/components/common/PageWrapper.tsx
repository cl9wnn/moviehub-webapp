import React from "react";

interface PageWrapperProps {
  children: React.ReactNode;
  backgroundClass?: string;
}

const PageWrapper: React.FC<PageWrapperProps> = ({
                                                   children,
                                                   backgroundClass = "bg-white",
                                                 }) => {
  return (
    <div className={`max-w-6xl mx-auto p-6 mt-16 ${backgroundClass}`}>
      {children}
    </div>
  );
};

export default PageWrapper;