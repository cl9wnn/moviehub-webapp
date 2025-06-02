import React from "react";
import TagButton from "../common/TagButton.tsx";

type AccountTitleProps = {
  username: string;
  isCurrentUser: boolean;
  onUploadAvatar: () => void;
}

const AccountTitle: React.FC<AccountTitleProps> = ({ username, isCurrentUser, onUploadAvatar}) => {
  return (
    <div className="space-y-4">
      <h1 className="text-5xl font-bold">{username}</h1>
      <div className="flex gap-4 flex-wrap">
        {isCurrentUser && (
        <TagButton onClick={onUploadAvatar}>
          Загрузить аватар
        </TagButton>)}
      </div>
    </div>
  );
};

export default AccountTitle;