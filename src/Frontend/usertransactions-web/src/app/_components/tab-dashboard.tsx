"use client";

import { Avatar, AvatarFallback } from "@/components/ui/avatar";
import { Badge } from "@/components/ui/badge";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { TabsContent } from "@/components/ui/tabs";
import { Skeleton } from "@/components/ui/skeleton";
import { ArrowRightLeft, CheckCircle, TrendingUp, Users, Wallet } from "lucide-react";
import { useEffect, useState } from "react";
import { useApp } from "@/contexts/AppContext";
import { Transaction, HealthStatus } from "@/lib/types";
import { getInitials } from "@/lib/utils";

export function TabDashboard() {
  const { 
    getUsersCount, 
    getWalletsCount, 
    getTransactionsCount, 
    getTransactionsTotalAmount,
    getLatestTransactions,
    getHealthStatus
  } = useApp();
  
  const [stats, setStats] = useState({
    usersCount: { totalQuantity: 0 },
    walletsCount: { totalQuantity: 0 },
    transactionsCount: { totalQuantity: 0 },
    totalAmount: { totalAmount: 0 }
  });
  
  const [latestTransactions, setLatestTransactions] = useState<Transaction[]>([]);
  const [healthStatus, setHealthStatus] = useState<HealthStatus>({
    mainApi: 'offline',
    services: []
  });
  
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const loadStats = async () => {
      setLoading(true);
      try { 
        const [usersCount, walletsCount, transactionsCount, totalAmount] = await Promise.all([
          getUsersCount(),
          getWalletsCount(), 
          getTransactionsCount(),
          getTransactionsTotalAmount()
        ]);
        
        setStats({
          usersCount,
          walletsCount,
          transactionsCount,
          totalAmount
        });
        
        const latest = await getLatestTransactions();
        setLatestTransactions(latest || []);
        
        const health = await getHealthStatus();
        setHealthStatus(health);
      } catch (error) {
        console.log('Erro ao carregar estatísticas:', error);
      } finally {
        setLoading(false);
      }
    };

    loadStats();
  }, [getUsersCount, getWalletsCount, getTransactionsCount, getTransactionsTotalAmount, getLatestTransactions, getHealthStatus]);

  const StatsCardSkeleton = () => (
    <Card>
      <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
        <Skeleton className="h-4 w-32" />
        <Skeleton className="h-4 w-4" />
      </CardHeader>
      <CardContent>
        <Skeleton className="h-8 w-16" />
      </CardContent>
    </Card>
  );

  const TransactionSkeleton = () => (
    <div className="flex items-center justify-between">
      <div className="flex items-center space-x-3">
        <Skeleton className="h-8 w-8 rounded-full" />
        <div>
          <Skeleton className="h-4 w-40 mb-1" />
          <Skeleton className="h-3 w-32" />
        </div>
      </div>
      <div className="flex items-center space-x-2">
        <Skeleton className="h-4 w-16" />
        <Skeleton className="h-4 w-4" />
      </div>
    </div>
  );

  const TransactionCardSkeleton = () => (
    <Card>
      <CardHeader>
        <Skeleton className="h-6 w-48" />
        <Skeleton className="h-4 w-64" />
      </CardHeader>
      <CardContent>
        <div className="space-y-4">
          {Array.from({ length: 3 }).map((_, index) => (
            <TransactionSkeleton key={index} />
          ))}
        </div>
      </CardContent>
    </Card>
  );

  const HealthServiceSkeleton = () => (
    <div className="flex items-center justify-between">
      <Skeleton className="h-4 w-20" />
      <Skeleton className="h-5 w-16" />
    </div>
  );

  const HealthCardSkeleton = () => (
    <Card>
      <CardHeader>
        <Skeleton className="h-6 w-40" />
        <Skeleton className="h-4 w-48" />
      </CardHeader>
      <CardContent>
        <div className="space-y-4">
          {Array.from({ length: 4 }).map((_, index) => (
            <HealthServiceSkeleton key={index} />
          ))}
        </div>
      </CardContent>
    </Card>
  );

  return (
    <TabsContent value="dashboard" className="space-y-6">
      <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-4">
        {loading ? (
          Array.from({ length: 4 }).map((_, index) => (
            <StatsCardSkeleton key={index} />
          ))
        ) : (
          <>
            <Card>
              <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                <CardTitle className="text-sm font-medium">Total de Usuários</CardTitle>
                <Users className="h-4 w-4 text-muted-foreground" />
              </CardHeader>
              <CardContent>
                <div className="text-2xl font-bold">{stats.usersCount.totalQuantity}</div>
              </CardContent>
            </Card>
            <Card>
              <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                <CardTitle className="text-sm font-medium">Carteiras Ativas</CardTitle>
                <Wallet className="h-4 w-4 text-muted-foreground" />
              </CardHeader>
              <CardContent>
                <div className="text-2xl font-bold">{stats.walletsCount.totalQuantity}</div>
              </CardContent>
            </Card>
            <Card>
              <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                <CardTitle className="text-sm font-medium">Transações Realizadas</CardTitle>
                <ArrowRightLeft className="h-4 w-4 text-muted-foreground" />
              </CardHeader>
              <CardContent>
                <div className="text-2xl font-bold">{stats.transactionsCount.totalQuantity}</div>
              </CardContent>
            </Card>
            <Card>
              <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                <CardTitle className="text-sm font-medium">Volume Total</CardTitle>
                <TrendingUp className="h-4 w-4 text-muted-foreground" />
              </CardHeader>
              <CardContent>
                <div className="text-2xl font-bold">R$ {stats.totalAmount.totalAmount.toFixed(2)}</div>
              </CardContent>
            </Card>
          </>
        )}
      </div>

      <div className="grid gap-4 md:grid-cols-2">
        {loading ? (
          <>
            <TransactionCardSkeleton />
            <HealthCardSkeleton />
          </>
        ) : (
          <>
            <Card>
              <CardHeader>
                <CardTitle>Transações Recentes</CardTitle>
                <CardDescription>Últimas transações processadas</CardDescription>
              </CardHeader>
              <CardContent>
                <div className="space-y-4">
                  {latestTransactions.length > 0 ? (
                    latestTransactions.map((transaction) => (
                      <div key={transaction.createdAt.toString()} className="flex items-center justify-between">
                        <div className="flex items-center space-x-3">
                          <Avatar className="h-8 w-8">
                            <AvatarFallback>
                              {getInitials(transaction.senderName!)}
                            </AvatarFallback>
                          </Avatar>
                          <div>
                            <p className="text-sm font-medium">
                              {transaction.senderName} → {transaction.receiverName}
                            </p>
                            <p className="text-xs text-muted-foreground">
                              {new Date(transaction.createdAt).toLocaleString('pt-BR')}
                            </p>
                          </div>
                        </div>
                        <div className="flex items-center space-x-2">
                          <span className="text-sm font-medium">R$ {transaction.amount.toFixed(2)}</span>
                          <CheckCircle className="h-4 w-4 text-green-500" />
                        </div>
                      </div>
                    ))
                  ) : (
                    <p className="text-sm text-muted-foreground">Nenhuma transação encontrada</p>
                  )}
                </div>
              </CardContent>
            </Card>

            <Card>
              <CardHeader>
                <CardTitle>Status do Sistema</CardTitle>
                <CardDescription>Monitoramento dos serviços</CardDescription>
              </CardHeader>
              <CardContent>
                <div className="space-y-4">
                  <div className="flex items-center justify-between">
                    <span className="text-sm">API Principal</span>
                    <Badge 
                      variant="outline" 
                      className={
                        healthStatus.mainApi === 'online' 
                          ? "text-green-600 border-green-600 animate-pulse"
                          : "text-red-600 border-red-600"
                      }
                    >
                      {healthStatus.mainApi === 'online' ? 'Online' : 'Offline'}
                    </Badge>
                  </div>
                  
                  {healthStatus.services.map((service) => (
                    <div key={service.service} className="flex items-center justify-between">
                      <span className="text-sm capitalize">
                        {service.service === 'sqlServer' ? 'SQL Server' : 
                         service.service === 'kafka-ui' ? 'Kafka UI' : 
                         service.service.charAt(0).toUpperCase() + service.service.slice(1)}
                      </span>
                      <Badge 
                        variant="outline" 
                        className={
                          service.status === 'healthy' 
                            ? "text-green-600 border-green-600 animate-pulse"
                            : "text-red-600 border-red-600"
                        }
                      >
                        {service.status === 'healthy' ? 'Online' : 'Offline'}
                      </Badge>
                    </div>
                  ))}
                  
                  {healthStatus.services.length === 0 && healthStatus.mainApi === 'offline' && (
                    <p className="text-sm text-muted-foreground">
                      Não foi possível verificar o status dos serviços
                    </p>
                  )}
                </div>
              </CardContent>
            </Card>
          </>
        )}
      </div>
    </TabsContent>
  );
}