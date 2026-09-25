import { useEffect, useState } from 'react'
import './App.css'

import { Api, type User } from "../Api.ts"
import { toast } from 'react-hot-toast/headless';
import { Toaster } from 'react-hot-toast';

const MyApi = new Api();
function App()
{
  const [users, setUsers] = useState<User[]>([]);
  useEffect(() =>
  {
    MyApi.getUsers.userGetUsers().then((response) =>
    {
      const data = response.data;
      setUsers(data);
    })


  }, []);



  function createUser(): void
  {

    MyApi.createUser.userCreateUser({
      FirstName: "",
      LastName: "Doe",
      Email: "best email ever",
      PhoneNumber: "123-456-7890",
      HomeAddress: "123 Main St",
      AddressCity: "Anytown",
      AddressState: "CA",
      AddressZipCode: "12345"
    }).then((response) =>
    {
      const createdUser = response.data;
      setUsers((prevUsers) => [...prevUsers, createdUser]);
      console.log("User created successfully");
    }).catch((error) =>
    {
      toast('Error Creating User')
      console.error("Error creating user:", error.error.title);
    });
  }

  return (<div>
    {users.map((user) => (
      <div key={user.userId}>
        <p>{user.firstName}</p>
      </div>
    ))}

    <button onClick={createUser}>Create User</button>

    <button onClick={() => toast.success("Connected!")}>
      Click Me to Test Toast
    </button>
  </div>
  )
}

export default App
