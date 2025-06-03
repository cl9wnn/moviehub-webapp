import React from "react";

type AccountInfoProps = {
  bio: string;
  registrationDate: string;
};


const AccountInfo : React.FC<AccountInfoProps> = ({bio, registrationDate}) => {
  return (
    <div className="space-y-2">
      <div className="space-y-2">
        <span className="font-semibold">Дата регистрации: </span>
        <span>{registrationDate}</span>
      </div>
      {bio != null && (
        <div className="space-y-2">
          <span className="font-semibold">О себе: </span>
          <span className="break-words whitespace-pre-wrap block">{bio}</span>
        </div>
      )}
    </div>
  );
};

export default AccountInfo;