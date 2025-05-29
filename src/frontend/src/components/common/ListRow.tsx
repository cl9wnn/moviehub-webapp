import React from "react";

const ListRow = ({ label, children }: { label: string, children: React.ReactNode }) => (
  <div className="flex items-start">
    <div className="w-1/3 font-semibold">{label}</div>
    <ul className="w-2/3 space-y-1 text-gray-700 pl-5">{children}</ul>
  </div>
);

export default ListRow;