import React, { useEffect, useState } from 'react';
import { getTestMessage } from '../services/getTestMessage';

const MainPage: React.FC = () => {
  const [message, setMessage] = useState('');
  const [timestamp, setTimestamp] = useState('');

  useEffect(() => {
    const fetchData = async () => {
      try {
        const data = await getTestMessage();
        setMessage(data.message);
        setTimestamp(data.timestamp);
      } catch (error) {
        console.error('Ошибка при получении данных:', error);
      }
    };

    fetchData();
  }, []);

  return (
    <div>
      <div className="text-2xl font-bold text-red-500">Hello Tailwind</div>
      <h1>Welcome to the Main Page</h1>
      <p>{message}</p>
      <p>{timestamp}</p>
    </div>
  );
};

export default MainPage;