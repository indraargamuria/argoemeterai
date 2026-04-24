import { useEffect, useState } from "react";
import {
  getCustomers,
  createCustomer
} from "./api/customers";

function App() {

  const [customers,setCustomers]=useState<any[]>([]);
  const [name,setName]=useState("");

  async function loadCustomers(){
    const res = await getCustomers();
    setCustomers(res.data);
  }

  useEffect(()=>{
    loadCustomers();
  },[]);

  async function addCustomer(){

    await createCustomer({
      customerCode:
       "CUST"+ Date.now(),
      name:name,
      pinHash:"1234"
    });

    setName("");

    await loadCustomers();
  }

  return (
   <div style={{padding:30}}>
    <h1>OpexNow</h1>

    <h2>Create Customer</h2>

    <input
      value={name}
      onChange={e=>setName(
        e.target.value
      )}
    />

    <button onClick={addCustomer}>
      Save
    </button>

    <h2>Customers</h2>

    <ul>
      {customers.map(c=>(
        <li key={c.id}>
          {c.customerCode}
          {" - "}
          {c.name}
        </li>
      ))}
    </ul>

   </div>
  )
}

export default App;