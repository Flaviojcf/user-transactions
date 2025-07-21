import { Badge } from "@/components/ui/badge";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import {
  Select,
  SelectTrigger,
  SelectValue,
  SelectContent,
  SelectItem,
} from "@/components/ui/select";
import { TabsContent } from "@/components/ui/tabs";
import { CheckCircle, XCircle, Clock } from "lucide-react";
import { useState } from "react";

export default function TabTransactions() {
  const [transactionForm, setTransactionForm] = useState({
    amount: "",
    senderId: "",
    receiverId: "",
  });

  const users = [
    {
      id: "1",
      name: "João Silva",
      email: "joao@example.com",
      type: "User",
      cpf: "123.456.789-01",
      balance: 750.0,
    },
    {
      id: "2",
      name: "Maria Santos",
      email: "maria@example.com",
      type: "Merchant",
      cpf: "987.654.321-09",
      balance: 1250.0,
    },
    {
      id: "3",
      name: "Pedro Costa",
      email: "pedro@example.com",
      type: "User",
      cpf: "456.789.123-45",
      balance: 320.5,
    },
  ];

  const transactions = [
    {
      id: "1",
      from: "João Silva",
      to: "Maria Santos",
      amount: 150.0,
      status: "Completed",
      date: "2024-01-15 14:30",
      type: "transfer",
    },
    {
      id: "2",
      from: "Pedro Costa",
      to: "João Silva",
      amount: 75.5,
      status: "Completed",
      date: "2024-01-15 13:15",
      type: "transfer",
    },
    {
      id: "3",
      from: "João Silva",
      to: "Pedro Costa",
      amount: 200.0,
      status: "Failed",
      date: "2024-01-15 12:00",
      type: "transfer",
    },
    {
      id: "4",
      from: "Maria Santos",
      to: "João Silva",
      amount: 300.0,
      status: "Pending",
      date: "2024-01-15 11:45",
      type: "transfer",
    },
  ];

  const handleTransactionSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    console.log("Transaction:", transactionForm);
    setTransactionForm({ amount: "", senderId: "", receiverId: "" });
  };

  return (
    <TabsContent value="transactions" className="space-y-6">
      <div className="grid gap-6 md:grid-cols-2">
        <Card>
          <CardHeader>
            <CardTitle>Nova Transação</CardTitle>
            <CardDescription>Transferir valor entre carteiras</CardDescription>
          </CardHeader>
          <CardContent>
            <form onSubmit={handleTransactionSubmit} className="space-y-4">
              <div className="space-y-2">
                <Label htmlFor="amount">Valor (R$)</Label>
                <Input
                  id="amount"
                  type="number"
                  step="0.01"
                  value={transactionForm.amount}
                  onChange={(e) =>
                    setTransactionForm({
                      ...transactionForm,
                      amount: e.target.value,
                    })
                  }
                  placeholder="100.50"
                  required
                />
              </div>
              <div className="space-y-2">
                <Label htmlFor="senderId">Remetente</Label>
                <Select
                  value={transactionForm.senderId}
                  onValueChange={(value) =>
                    setTransactionForm({ ...transactionForm, senderId: value })
                  }
                >
                  <SelectTrigger>
                    <SelectValue placeholder="Selecione o remetente" />
                  </SelectTrigger>
                  <SelectContent>
                    {users
                      .filter((u) => u.type === "User")
                      .map((user) => (
                        <SelectItem key={user.id} value={user.id}>
                          {user.name} - R$ {user.balance.toFixed(2)}
                        </SelectItem>
                      ))}
                  </SelectContent>
                </Select>
              </div>
              <div className="space-y-2">
                <Label htmlFor="receiverId">Destinatário</Label>
                <Select
                  value={transactionForm.receiverId}
                  onValueChange={(value) =>
                    setTransactionForm({
                      ...transactionForm,
                      receiverId: value,
                    })
                  }
                >
                  <SelectTrigger>
                    <SelectValue placeholder="Selecione o destinatário" />
                  </SelectTrigger>
                  <SelectContent>
                    {users.map((user) => (
                      <SelectItem key={user.id} value={user.id}>
                        {user.name} (
                        {user.type === "User" ? "Usuário" : "Lojista"})
                      </SelectItem>
                    ))}
                  </SelectContent>
                </Select>
              </div>
              <Button type="submit" className="w-full">
                Processar Transação
              </Button>
            </form>
          </CardContent>
        </Card>

        <Card>
          <CardHeader>
            <CardTitle>Histórico de Transações</CardTitle>
            <CardDescription>Todas as transações processadas</CardDescription>
          </CardHeader>
          <CardContent>
            <div className="space-y-4">
              {transactions.map((transaction) => (
                <div
                  key={transaction.id}
                  className="flex items-center justify-between p-3 border rounded-lg"
                >
                  <div className="flex items-center space-x-3">
                    <div className="flex items-center">
                      {transaction.status === "Completed" && (
                        <CheckCircle className="h-5 w-5 text-green-500" />
                      )}
                      {transaction.status === "Failed" && (
                        <XCircle className="h-5 w-5 text-red-500" />
                      )}
                      {transaction.status === "Pending" && (
                        <Clock className="h-5 w-5 text-yellow-500" />
                      )}
                    </div>
                    <div>
                      <p className="font-medium text-sm">
                        {transaction.from} → {transaction.to}
                      </p>
                      <p className="text-xs text-muted-foreground">
                        {transaction.date}
                      </p>
                    </div>
                  </div>
                  <div className="text-right">
                    <p className="font-medium">
                      R$ {transaction.amount.toFixed(2)}
                    </p>
                    <Badge
                      variant={
                        transaction.status === "Completed"
                          ? "default"
                          : transaction.status === "Failed"
                          ? "destructive"
                          : "secondary"
                      }
                      className="text-xs"
                    >
                      {transaction.status === "Completed"
                        ? "Concluída"
                        : transaction.status === "Failed"
                        ? "Falhou"
                        : "Pendente"}
                    </Badge>
                  </div>
                </div>
              ))}
            </div>
          </CardContent>
        </Card>
      </div>
    </TabsContent>
  );
}
