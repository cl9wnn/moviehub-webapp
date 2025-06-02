import React, {useEffect, useRef, useState} from "react";
import {useParams} from "react-router-dom";
import type {UserResponse} from "../models/user.ts";
import {getUserById} from "../services/users/getUserById.ts";
import PageWrapper from "../components/common/PageWrapper.tsx";
import Header from "../components/common/Header.tsx";
import Avatar from "../components/account/Avatar.tsx";
import AccountTitle from "../components/account/AccountTitle.tsx";
import {uploadAvatar} from "../services/users/uploadAvatar.ts";


const AccountPage: React.FC = () => {
  const { userId } = useParams();
  const [user, setUser] = useState<UserResponse | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const fileInputRef = useRef<HTMLInputElement | null>(null);

  const handleUploadClick = () => {
    fileInputRef.current?.click();
  };

  const handleFileChange = async (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0];
    if (!file) return;

    try {
      await uploadAvatar(file);
      if (userId) {
        const updatedUser = await getUserById(userId);
        setUser(updatedUser);
      }
    } catch (err) {
      if (err instanceof Error) {
        setError(err.message);
      } else {
        setError("Ошибка при загрузке аватара.");
      }
    }
  };

  useEffect(() => {
    if (!userId) {
      setError("Empty ID in URL");
      setLoading(false);
      return;
    }

    const fetchData = async () => {
      try {
        const userResponse = await getUserById(userId);
        setUser(userResponse);
      } catch (err) {
        if (err instanceof Error) {
          setError(err.message);
        } else {
          setError("Something went wrong. Please try again.");
        }
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [userId]);

  if (loading) return <div className="text-center mt-10 text-lg">Загрузка...</div>;
  if (error) return <div className="text-center mt-10 text-red-600">{error}</div>;

  return (
    <>
      <Header/>
      <PageWrapper>
        <div className="flex flex-col items-center">
          <div className="w-full flex flex-col lg:flex-row gap-10 justify-center">
            <Avatar  photoUrl={user?.avatarUrl ? `${user.avatarUrl}?t=${new Date().getTime()}` : undefined}
                     name={user?.username} />

            <div className="flex-1 max-w-3xl space-y-8">
              {user && (
                <AccountTitle
                  isCurrentUser={user.isCurrentUser}
                  username={user.username}
                  onUploadAvatar={handleUploadClick}
                />
              )}
              <input
                ref={fileInputRef}
                type="file"
                accept="image/*"
                className="hidden"
                onChange={handleFileChange}
              />
            </div>
          </div>
        </div>
      </PageWrapper>
    </>
  );
}

export default AccountPage;