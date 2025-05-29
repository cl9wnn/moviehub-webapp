import React from "react";

const InfoRow = ({ label, value }: { label: string, value: React.ReactNode }) => (
  <div className="flex">
    <div className="w-1/3 font-semibold">{label}</div>
    <div className="w-2/3 text-gray-800 pl-5">{value}</div>
  </div>
);

export default InfoRow;