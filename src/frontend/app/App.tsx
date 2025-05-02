import {useEffect, useState } from 'react';
import {getTestMessage} from "../services/getTestMessage.ts";

function App() {
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
      <p>{message}</p>
      <p>{timestamp}</p>
    </div>
  );
}

export default App
