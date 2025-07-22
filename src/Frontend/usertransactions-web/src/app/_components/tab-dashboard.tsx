import { Avatar, AvatarFallback } from "@/components/ui/avatar";
import { Badge } from "@/components/ui/badge";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { TabsContent } from "@/components/ui/tabs";
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

  useEffect(() => {
    const loadStats = async () => {
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
      }
    };

    loadStats();
  }, [getUsersCount, getWalletsCount, getTransactionsCount, getTransactionsTotalAmount, getLatestTransactions, getHealthStatus]);

  
    return (
        <TabsContent value="dashboard" className="space-y-6">
            <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-4">
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
                  { <div className="text-2xl font-bold">R$ {stats.totalAmount.totalAmount.toFixed(2)}</div> }
                </CardContent>
              </Card>
            </div>

            <div className="grid gap-4 md:grid-cols-2">
              <Card>
                <CardHeader>
                  <CardTitle>Transações Recentes</CardTitle>
                  <CardDescription>Últimas transações processadas</CardDescription>
                </CardHeader>
                <CardContent>
                  <div className="space-y-4">
                    {latestTransactions.map((transaction) => (
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
                    ))}
                    {latestTransactions.length === 0 && (
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
            </div>
          </TabsContent>
    )
}