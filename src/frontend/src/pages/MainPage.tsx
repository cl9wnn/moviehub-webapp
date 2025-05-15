import React, { useEffect, useState } from 'react';
import { getTestProtectedResource } from '../services/getTestProtectedResource.ts';
import Header from "../components/common/Header.tsx";

const MainPage: React.FC = () => {
  const [message, setMessage] = useState('');
  const [timestamp, setTimestamp] = useState('');

  useEffect(() => {
    const fetchData = async () => {
      try {
        const data = await getTestProtectedResource();
        setMessage(data.message);
        setTimestamp(data.timestamp);
      } catch (error) {
        console.error('Ошибка при получении данных:', error);
      }
    };

    fetchData();
  }, []);

  return (
    <>
    <Header/>
    <div className="pt-20 px-4">
      <div className="text-2xl font-bold text-red-500">Hello, World</div>
      <p>{message}</p>
      <p>{timestamp}</p>
    </div>
      </>
  );
};

export default MainPage;