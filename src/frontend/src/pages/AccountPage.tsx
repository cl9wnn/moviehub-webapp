import React, {useEffect, useState} from "react";
import {useParams} from "react-router-dom";
import type {UserResponse} from "../models/user.ts";
import {getUserById} from "../services/users/getUserById.ts";


const AccountPage: React.FC = () => {
  const { userId } = useParams();
  const [user, setUser] = useState<UserResponse | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

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
      <div>
        <p>{user?.username}</p>

        {user?.isCurrentUser && (
          <p>Мой профиль</p>
        )}
      </div>
  );
}

export default AccountPage;