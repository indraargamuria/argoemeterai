import api from "./client";

export const getCustomers = () =>
  api.get("/customers");

export const createCustomer = (payload:any) =>
  api.post("/customers", payload);