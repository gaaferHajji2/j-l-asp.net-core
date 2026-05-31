export default function ChatRoom({ messages }: { messages: string[]}) {
  return (
      <div className="flex-1 overflow-y-auto p-4 space-y-4 sm:p-6">
        {messages.length === 0 ? (
          <div className="flex flex-col items-center justify-center h-full text-gray-400">
            <svg xmlns="http://www.w3.org/2000/svg" className="h-12 w-12 mb-3 opacity-50" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M8 12h.01M12 12h.01M16 12h.01M21 12c0 4.418-4.03 8-9 8a9.863 9.863 0 01-4.255-.949L3 20l1.395-3.72C3.512 15.042 3 13.574 3 12c0-4.418 4.03-8 9-8s9 3.582 9 8z" />
            </svg>
            <p>No messages yet. Waiting for incoming data...</p>
          </div>
        ) : (
          messages.map((msg: string, index: number) => (
            <div
              key={index}
              className="flex flex-col space-y-1 animate-fade-in-up"
            >
              {/* Message Bubble */}
              <div className="flex items-start gap-3 max-w-3xl">

                {/* Avatar Placeholder */}
                <div className="shrink-0 w-10 h-10 rounded-full bg-linear-to-br from-indigo-500 to-purple-600 flex items-center justify-center text-white font-bold shadow-md">
                  {msg.split(":")[0].charAt(0).toUpperCase()}
                </div>

                {/* Content */}
                <div className="flex flex-col">
                  <div className="flex items-baseline gap-2">
                    <span className="font-semibold text-gray-900 text-sm">
                      {msg.split(":")[0]}
                    </span>
                  </div>

                  <div className="mt-1 bg-white border border-gray-100 shadow-sm rounded-2xl rounded-tl-none px-4 py-2.5 text-gray-700 text-sm leading-relaxed wrap-break-words">
                    {msg.split(":")[1]}
                  </div>
                </div>
              </div>
            </div>
          ))
        )}
      </div>
  )
}
