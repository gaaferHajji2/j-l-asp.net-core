import './App.css'
import WaitingRoom from './components/WaitingRoom';

function App() {
  return (
    <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
      <div className="px-5 my-5 w-full">
        <h1 className="text-4xl font-light">Welcome to JLoka Chat App</h1>
      </div>

      <WaitingRoom joinChatRoom={(username: string, chatRoom:string) => {
        console.log(`The username is: ${username} & chatRoom is: ${chatRoom}`)
      }}/>
    </div>
  );
}

export default App
