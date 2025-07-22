"use client";

import React, {
  createContext,
  useContext,
  useState,
  useEffect,
  ReactNode,
} from "react";
import {
  User,
  Wallet,
  Transaction,
  TotalQuantityResponse,
  TotalAmountResponse,
  HealthStatus,
  HealthService,
  ApiErrorResponse,
} from "@/lib/types";
import { ApiService } from "@/lib/api";
import { handleApiError, showSuccessMessage } from "@/lib/utils";

interface AppContextType {
  users: User[];
  wallets: Wallet[];
  transactions: Transaction[];
  loading: boolean;

  createUser: (userData: {
    fullName: string;
    email: string;
    cpf: string;
    password: string;
    userType: number;
  }) => Promise<void>;

  createWallet: (userId: string) => Promise<void>;

  createTransaction: (transactionData: {
    amount: number;
    senderId: string;
    receiverId: string;
  }) => Promise<void>;

  refreshData: () => Promise<void>;

  getUsersCount: () => Promise<TotalQuantityResponse>;
  getWalletsCount: () => Promise<TotalQuantityResponse>;
  getTransactionsCount: () => Promise<TotalQuantityResponse>;
  getTransactionsTotalAmount: () => Promise<TotalAmountResponse>;
  getLatestTransactions: () => Promise<Transaction[]>;
  getHealthStatus: () => Promise<HealthStatus>;
}

const AppContext = createContext<AppContextType | undefined>(undefined);

export function AppProvider({ children }: { children: ReactNode }) {
  const [users, setUsers] = useState<User[]>([]);
  const [wallets, setWallets] = useState<Wallet[]>([]);
  const [transactions, setTransactions] = useState<Transaction[]>([]);
  const [loading, setLoading] = useState(false);

  const refreshData = async () => {
    setLoading(true);

    try {
      const [usersData, walletsData, transactionsData] = await Promise.all([
        ApiService.getAllUsers().catch(() => [] as User[]),
        ApiService.getAllWallets().catch(() => [] as Wallet[]),
        ApiService.getAllTransactions().catch(() => [] as Transaction[]),
      ]);

      setUsers(Array.isArray(usersData) ? usersData : []);
      setWallets(Array.isArray(walletsData) ? walletsData : []);
      setTransactions(Array.isArray(transactionsData) ? transactionsData : []);
    } catch (err) {
      handleApiError(err as ApiErrorResponse);
    } finally {
      setLoading(false);
    }
  };

  const createUser = async (userData: {
    fullName: string;
    email: string;
    cpf: string;
    password: string;
    userType: number;
  }) => {
    setLoading(true);

    try {
      await ApiService.createUser({
        FullName: userData.fullName,
        Email: userData.email,
        CPF: userData.cpf,
        Password: userData.password,
        UserType: userData.userType,
      });

      const usersData = await ApiService.getAllUsers();
      setUsers(Array.isArray(usersData) ? usersData : []);
      showSuccessMessage("Usuário cadastrado com sucesso!");
    } catch (err) {
      handleApiError(err as ApiErrorResponse);
      throw err;
    } finally {
      setLoading(false);
    }
  };

  const createWallet = async (userId: string) => {
    setLoading(true);

    try {
      await ApiService.createWallet({ UserId: userId });

      const walletsData = await ApiService.getAllWallets();
      setWallets(Array.isArray(walletsData) ? walletsData : []);
      showSuccessMessage("Carteira criada com sucesso!");
    } catch (err) {
      handleApiError(err as ApiErrorResponse);
      throw err;
    } finally {
      setLoading(false);
    }
  };

  const createTransaction = async (transactionData: {
    amount: number;
    senderId: string;
    receiverId: string;
  }) => {
    setLoading(true);

    try {
      await ApiService.createTransaction(transactionData);

      const [transactionsData, walletsData] = await Promise.all([
        ApiService.getAllTransactions(),
        ApiService.getAllWallets(),
      ]);

      setTransactions(Array.isArray(transactionsData) ? transactionsData : []);
      setWallets(Array.isArray(walletsData) ? walletsData : []);
      showSuccessMessage("Transação realizada com sucesso!");
    } catch (err) {
      handleApiError(err as ApiErrorResponse);
      throw err;
    } finally {
      setLoading(false);
    }
  };

  const getUsersCount = async () => {
    try {
      return await ApiService.getUsersCount();
    } catch {
      return { totalQuantity: 0 }; ;
    }
  };

  const getWalletsCount = async (): Promise<TotalQuantityResponse> => {
    try {
      return await ApiService.getWalletsCount();
    } catch {
      return { totalQuantity: 0 }; 
    }
  };

  const getTransactionsCount = async () => {
    try {
      return await ApiService.getTransactionsCount();
    } catch {
      return { totalQuantity: 0 }; ;
    }
  };

  const getTransactionsTotalAmount = async () => {
    try {
      return await ApiService.getTransactionsTotalAmount();
    } catch {
      return { totalAmount: 0 }; ;
    }
  };

  const getLatestTransactions = async (): Promise<Transaction[]> => {
    try {
      const result = await ApiService.getLatestFourTransactions();
      return Array.isArray(result) ? result : [];
    } catch {
      return [];
    }
  };

  const getHealthStatus = async (): Promise<HealthStatus> => {
    try {
      const [mainHealthResponse, servicesHealthResponse] = await Promise.all([
        ApiService.getMainHealthStatus().catch(() => null),
        ApiService.getServicesHealthStatus().catch(() => null)
      ]);

      const mainApi = mainHealthResponse === 'Healthy' ? 'online' : 'offline';
      
      let services: HealthService[] = [];
      if (servicesHealthResponse && servicesHealthResponse.services) {
        services = servicesHealthResponse.services.map(service => ({
          ...service,
          status: service.status as 'healthy' | 'unhealthy'
        }));
      }

      return {
        mainApi,
        services
      };
    } catch {
      return {
        mainApi: 'offline',
        services: []
      };
    }
  };

  useEffect(() => {
    refreshData();
  }, []);

  const value: AppContextType = {
    users,
    wallets,
    transactions,
    loading,
    createUser,
    createWallet,
    createTransaction,
    refreshData,
    getUsersCount,
    getWalletsCount,
    getTransactionsCount,
    getTransactionsTotalAmount,
    getLatestTransactions,
    getHealthStatus,
  };

  return <AppContext.Provider value={value}>{children}</AppContext.Provider>;
}

export function useApp() {
  const context = useContext(AppContext);
  if (context === undefined) {
    throw new Error("useApp must be used within an AppProvider");
  }
  return context;
}
