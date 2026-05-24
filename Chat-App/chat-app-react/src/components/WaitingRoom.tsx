import { useState } from "react"

export default function WaitingRoom({ joinChatRoom }) {

  const [username, setUsername] = useState()
  const [chatRoom, setChatRoom] = useState()

  return (
    <>
      <form className="space-y-6 max-w-md mx-auto p-6 bg-white rounded-xl 
      shadow-sm border border-gray-200" onSubmit={(e) => {
        e.preventDefault()
        joinChatRoom(username, chatRoom)
      }}>
      </form>
    </>
  )
}
