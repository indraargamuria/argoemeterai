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

  async function uploadFile(
    e:any
    ){
    const file=e.target.files[0];

    const formData=
      new FormData();

    formData.append(
      "file",
      file
    );

    await fetch(
    "http://localhost:5260/api/documents/upload",
    {
      method:"POST",
      body:formData
    });

    alert("Uploaded");
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

      <h2>Upload Document</h2>

      <input
      type="file"
      onChange={uploadFile}
      />
   </div>
  )
}

export default App;