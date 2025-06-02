import React from "react";

type AccountInfoProps = {
  bio: string;
  registrationDate: string;
};


const AccountInfo : React.FC<AccountInfoProps> = ({bio, registrationDate}) => {
  return (
    <div className="space-y-4">
      <div className="space-y-4">
        <span className="font-semibold">Дата регистрации: </span>
        <span>{registrationDate}</span>
      </div>
      <div className="space-y-4">
        <span className="font-semibold">О себе: </span>
        <span>{bio}</span>
      </div>
  </div>
  );
};

export default AccountInfo;