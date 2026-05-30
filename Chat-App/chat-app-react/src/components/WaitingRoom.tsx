import { useState, type SubmitEvent } from "react"

interface Props {
  joinChatRoom: (username: string, chatRoom: string) => void;
}

export default function WaitingRoom({ joinChatRoom }: Props) {

  const [username, setUsername] = useState<string>('')
  const [chatRoom, setChatRoom] = useState<string>('')

  const handleSubmit = (e: SubmitEvent<HTMLFormElement>) => {
    e.preventDefault()
    // Add your submission logic here

    if(username == '' || chatRoom == '' || username.trim() == '' || chatRoom.trim() == '') {
      alert("Please provide values for the username and chatRoom")
    } else {
      joinChatRoom(username, chatRoom)
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-linear-to-br from-indigo-100 via-purple-100 to-pink-100 p-4">
      <div className="w-full max-w-md bg-white rounded-2xl shadow-xl overflow-hidden transform transition-all hover:scale-[1.01] duration-300">
        
        {/* Header Section */}
        <div className="bg-indigo-600 p-6 text-center">
          <h2 className="text-2xl font-bold text-white tracking-wide">Join Conversation</h2>
          <p className="text-indigo-200 text-sm mt-1">Enter your details to start chatting</p>
        </div>

        {/* Form Section */}
        <form onSubmit={handleSubmit} className="p-8 space-y-6">
          
          {/* Username Field */}
          <div className="space-y-2">
            <label 
              htmlFor="username" 
              className="block text-sm font-medium text-gray-700 ml-1"
            >
              Username
            </label>
            <div className="relative group">
              <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                {/* User Icon */}
                <svg 
                  className="h-5 w-5 text-gray-400 group-focus-within:text-indigo-500 transition-colors duration-200" 
                  xmlns="http://www.w3.org/2000/svg" 
                  viewBox="0 0 20 20" 
                  fill="currentColor"
                >
                  <path fillRule="evenodd" d="M10 9a3 3 0 100-6 3 3 0 000 6zm-7 9a7 7 0 1114 0H3z" clipRule="evenodd" />
                </svg>
              </div>
              <input
                type="text"
                id="username"
                name="username"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
                placeholder="johndoe"
                className="block w-full pl-10 pr-3 py-3 border border-gray-300 rounded-lg leading-5 bg-gray-50 placeholder-gray-400 focus:outline-none focus:bg-white focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 transition-all duration-200 sm:text-sm"
                required
              />
            </div>
          </div>

          {/* Chatroom Field */}
          <div className="space-y-2">
            <label 
              htmlFor="chatroom" 
              className="block text-sm font-medium text-gray-700 ml-1"
            >
              Chatroom ID
            </label>
            <div className="relative group">
              <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                {/* Chat Icon */}
                <svg 
                  className="h-5 w-5 text-gray-400 group-focus-within:text-indigo-500 transition-colors duration-200" 
                  xmlns="http://www.w3.org/2000/svg" 
                  viewBox="0 0 20 20" 
                  fill="currentColor"
                >
                  <path fillRule="evenodd" d="M18 10c0 3.866-3.582 7-8 7a8.841 8.841 0 01-4.083-.98L2 17l1.338-3.123C2.493 12.767 2 11.434 2 10c0-3.866 3.582-7 8-7s8 3.134 8 7zM7 9H5v2h2V9zm8 0h-2v2h2V9zM9 9h2v2H9V9z" clipRule="evenodd" />
                </svg>
              </div>
              <input
                type="text"
                id="chatroom"
                name="chatroom"
                value={chatRoom}
                onChange={(e) => setChatRoom(e.target.value)}
                placeholder="general-lounge"
                className="block w-full pl-10 pr-3 py-3 border border-gray-300 rounded-lg leading-5 bg-gray-50 placeholder-gray-400 focus:outline-none focus:bg-white focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 transition-all duration-200 sm:text-sm"
                required
              />
            </div>
          </div>

          {/* Submit Button */}
          <button
            type="submit"
            className="w-full flex justify-center py-3 px-4 border border-transparent rounded-lg shadow-sm text-sm font-medium text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500 transition-colors duration-200 transform active:scale-95"
          >
            Enter Chat
          </button>
        </form>
      </div>
    </div>
  );
};

