import type { HubConnection } from '@microsoft/signalr';
import { useState, type SubmitEvent } from 'react';

const SendMessage = ({ conn }: { conn: HubConnection}) => {
  const [value, setValue] = useState<string>('');

  return (
    <form 
      onSubmit={
        async (e: SubmitEvent<HTMLFormElement>) => {
            e.preventDefault()
            
            await conn.invoke("SendMessage", value)
        }
    } 
      className="flex items-center max-w-md mx-auto bg-white rounded-full shadow-lg overflow-hidden border border-gray-100 p-1"
    >
      <input
        type="text"
        placeholder="Type something..."
        value={value}
        onChange={(e) => setValue(e.target.value.trim())}
        className="grow px-4 py-2 text-gray-700 bg-transparent outline-none placeholder-gray-400"
      />
      <button
        type="submit"
        className="px-6 py-2 text-white bg-indigo-600 rounded-full hover:bg-indigo-700 transition-colors duration-300 font-medium"
      >
        Send
      </button>
    </form>
  );
};

export default SendMessage;