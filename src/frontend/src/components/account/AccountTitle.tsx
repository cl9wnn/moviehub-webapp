import React from "react";
import TagButton from "../common/TagButton.tsx";

type AccountTitleProps = {
  username: string;
  isCurrentUser: boolean;
  onUploadAvatar: () => void;
  onPersonalizeProfile: () => void;
}

const AccountTitle: React.FC<AccountTitleProps> = ({ username, isCurrentUser, onUploadAvatar, onPersonalizeProfile }) => {
  return (
    <div className="space-y-4">
      <h1 className="text-5xl font-bold">{username}</h1>
      <div className="flex gap-1 flex-wrap">
        {isCurrentUser && (
          <>
            <TagButton onClick={onUploadAvatar} className="mr-2">
              Загрузить аватар
            </TagButton>
            <TagButton onClick={onPersonalizeProfile}>
              Персонализировать
            </TagButton>
          </>
        )}
      </div>
    </div>
  );
};

export default AccountTitle;