import {useContext} from "react";
import { AuthContext } from "../context/AuthContext.tsx";
import type {AuthContextType} from "../context/AuthContext.tsx";

export const useAuth = (): AuthContextType => {
  const context = useContext(AuthContext);
  if (!context) throw new Error("useAuth must be used within AuthProvider");
  return context;
};