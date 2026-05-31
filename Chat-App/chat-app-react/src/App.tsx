import { useState } from 'react';
import './App.css'
import WaitingRoom from './components/WaitingRoom'
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr'
import ChatRoom from './components/ChatRoom';

function App() {

  const [connection, setConnection] = useState<HubConnection>()
  const [messages, setMessages] = useState<string[]>([])

  const joinChatRoom = async (username: string, chatRoom: string) => {
    try {
      const conn = new HubConnectionBuilder()
        .withUrl("https://localhost:7260/Chat")
        .configureLogging(LogLevel.Information)
        .build()

      conn.on("JoinSpecificChatRoom", (username: string, msg: string) => {
        console.log("The username is: ", username, ", and the msg is: ", msg)
      })

      conn.on("ReceiveMessage", (username: string, msg: string) => {
        console.log("The username of receive msg is: ", username, ", and the msg is: ", msg)
      })

      conn.on("ReceiveSpecificMessage", (username: string, msg: string) => {
        setMessages(prev => [username + ": " + msg, ...prev])
      })

      await conn.start()
      await conn.invoke("JoinSpecificChatRoom", { username, chatRoom })
      setConnection(conn)
    } catch (e) {
      console.log("Error during the connection: ", e)
    }
  }

  return (
    <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
      <div className="px-5 my-5 w-full">
        <h1 className="text-4xl font-light">Welcome to JLoka Chat App</h1>
      </div>

      <WaitingRoom joinChatRoom={joinChatRoom} />

      <ChatRoom messages={messages} />
    </div>
  );
}

export default App
