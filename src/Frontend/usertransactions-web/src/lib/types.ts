export interface User {
  id: string;
  fullName: string;
  email: string;
  cpf: string;
  userType: number;
  createdAt: Date;
}

export interface Wallet {
  id: string;
  fullName: string;
  email: string;
  balance: number;
  userType: number;
}

export interface Transaction {
  id: string;
  senderName: string;
  receiverName: string;
  amount: number;
  createdAt: Date;
}

export interface CreateUserRequest {
  fullName: string;
  email: string;
  cpf: string;
  password: string;
  userType: number;
}

export interface CreateWalletRequest {
  userId: string;
}

export interface CreateTransactionRequest {
  amount: number;
  senderId: string;
  receiverId: string;
}

export interface DashboardStats {
  usersCount: number;
  walletsCount: number;
  transactionsCount: number;
  totalAmount: number;
}

export interface HealthService {
  service: string;
  status: "healthy" | "unhealthy";
}

export interface HealthCheckResponse {
  services: HealthService[];
}

export interface HealthStatus {
  mainApi: "online" | "offline";
  services: HealthService[];
}

export interface TotalQuantityResponse {
  totalQuantity: number;
}

export interface TotalAmountResponse {
  totalAmount: number;
}

export interface ApiErrorResponse {
  traceId: string;
  type: string;
  statusCode: number;
  errorDetails: {
    messages: string[];
  };
}
