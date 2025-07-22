import { TotalAmountResponse, TotalQuantityResponse } from "./types";

const API_BASE_URL = 'https://localhost:7035/v1';

export class ApiService {
  private static async request<T>(
    endpoint: string,
    options: RequestInit = {}
  ): Promise<T> {
    const url = `${API_BASE_URL}${endpoint}`;
    
    try {
      const response = await fetch(url, {
        headers: {
          'Content-Type': 'application/json',
          ...options.headers,
        },
        ...options,
      });

      if (!response.ok) {
        throw new Error(`API Error: ${response.status} ${response.statusText}`);
      }

      return response.json();
    } catch (error) {
      if (error instanceof TypeError && error.message.includes('fetch')) {
        throw new Error('Erro de conexão com a API. Verifique se o servidor está rodando em HTTPS.');
      }
      throw error;
    }
  }

  static async createUser(data: {
    FullName: string;
    Email: string;
    CPF: string;
    Password: string;
    UserType: number;
  }) {
    return this.request('/user/register', {
      method: 'POST',
      body: JSON.stringify(data),
    });
  }

  static async getAllUsers() {
    return this.request('/user/list-all');
  }

  static async getUsersCount() {
    return this.request<TotalQuantityResponse>('/user/list-total-quantity');
  }

  static async createWallet(data: { UserId: string }) {
    return this.request('/wallet/register', {
      method: 'POST',
      body: JSON.stringify(data),
    });
  }

  static async getAllWallets() {
    return this.request('/wallet/list-all');
  }

  static async getWalletsCount() {
    return this.request<TotalQuantityResponse>('/wallet/list-total-quantity');
  }

  static async createTransaction(data: {
    amount: number;
    senderId: string;
    receiverId: string;
  }) {
    return this.request('/transaction/register', {
      method: 'POST',
      body: JSON.stringify(data),
    });
  }

  static async getAllTransactions() {
    return this.request('/transaction/list-all');
  }

  static async getTransactionsCount() {
    return this.request<TotalQuantityResponse>('/transaction/list-total-quantity');
  }

  static async getTransactionsTotalAmount() {
    return this.request<TotalAmountResponse>('/transaction/list-total-amount');
  }

  static async getLatestFourTransactions() {
    return this.request('/transaction/list-latest-four');
  }

  static async getMainHealthStatus(): Promise<string> {
    const url = 'https://localhost:7035/health';
    try {
      const response = await fetch(url);
      if (!response.ok) throw new Error(`API Error: ${response.status} ${response.statusText}`);
      return response.text(); 
    } catch (error) {
      if (error instanceof TypeError && error.message.includes('fetch')) {
        throw new Error('Erro de conexão com a API principal');
      }
      throw error;
    }
  }

  static async getServicesHealthStatus() {
    return this.request<{ services: { service: string; status: string }[] }>('/health');
  }
}
